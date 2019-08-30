using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epic.IO
{
    public partial class StreamHelper
    {
        public static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };
        public static int UnseekableStreamInitialRentSize = 4096;

        public static ArraySegment<byte> ReadToEnd(Stream stream)
        {
            int written = 0;
            byte[] rented = null;

            var utf8Bom = Utf8Bom;

            try
            {
                if (stream.CanSeek)
                {
                    var expectedLength = Math.Max(utf8Bom.Length, stream.Length - stream.Position) + 1;
                    rented = ArrayPool<byte>.Shared.Rent(checked((int)expectedLength));
                }
                else
                    rented = ArrayPool<byte>.Shared.Rent(UnseekableStreamInitialRentSize);

                int lastRead;

                do
                {
                    Debug.Assert(rented.Length >= utf8Bom.Length);
                    lastRead = stream.Read(rented, written, utf8Bom.Length - written);
                    written += lastRead;
                } while (lastRead > 0 && written < utf8Bom.Length);

                if (written == utf8Bom.Length && utf8Bom.SequenceEqual(rented.AsSpan(0, utf8Bom.Length))) written = 0;

                do
                {
                    if (rented.Length == written)
                    {
                        byte[] toReturn = rented;
                        rented = ArrayPool<byte>.Shared.Rent(checked(toReturn.Length * 2));
                        Buffer.BlockCopy(toReturn, 0, rented, 0, toReturn.Length);
                        ArrayPool<byte>.Shared.Return(toReturn, clearArray: true);
                    }

                    lastRead = stream.Read(rented, written, rented.Length - written);
                    written += lastRead;
                } while (lastRead > 0);

                return new ArraySegment<byte>(rented, 0, written);
            }
            catch
            {
                if (rented != null)
                {
                    // Holds document content, clear it before returning it.
                    rented.AsSpan(0, written).Clear();
                    ArrayPool<byte>.Shared.Return(rented);
                }

                throw;
            }
        }

        public static async Task<ArraySegment<byte>> ReadToEndAsync(Stream stream, CancellationToken cancellationToken)
        {
            int written = 0;
            byte[] rented = null;

            try
            {
                int utf8BomLength = Utf8Bom.Length;

                if (stream.CanSeek)
                {
                    long expectedLength = Math.Max(utf8BomLength, stream.Length - stream.Position) + 1;
                    rented = ArrayPool<byte>.Shared.Rent(checked((int)expectedLength));
                }
                else
                    rented = ArrayPool<byte>.Shared.Rent(UnseekableStreamInitialRentSize);

                int lastRead;

                do
                {
                    Debug.Assert(rented.Length >= Utf8Bom.Length);

                    lastRead = await stream.ReadAsync(rented, written, utf8BomLength - written, cancellationToken).ConfigureAwait(false);
                    written += lastRead;
                } while (lastRead > 0 && written < utf8BomLength);

                if (written == utf8BomLength && Utf8Bom.SequenceEqual(rented.AsSpan(0, utf8BomLength))) written = 0;
 
                do
                {
                    if (rented.Length == written)
                    {
                        byte[] toReturn = rented;
                        rented = ArrayPool<byte>.Shared.Rent(toReturn.Length * 2);
                        Buffer.BlockCopy(toReturn, 0, rented, 0, toReturn.Length);
                        ArrayPool<byte>.Shared.Return(toReturn, clearArray: true);
                    }

                    lastRead = await stream.ReadAsync(rented, written, rented.Length - written, cancellationToken).ConfigureAwait(false);
                    written += lastRead;

                } while (lastRead > 0);

                return new ArraySegment<byte>(rented, 0, written);
            }
            catch
            {
                if (rented != null)
                {
                    rented.AsSpan(0, written).Clear();
                    ArrayPool<byte>.Shared.Return(rented);
                }

                throw;
            }
        }

    }
}
