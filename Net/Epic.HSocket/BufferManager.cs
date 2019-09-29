using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Epic.Net
{
    public class BufferManager
    {
        int total;
        byte[] buffer;
        Stack<int> freeIndexPool;
        int current;
        int size;

        public BufferManager(int total, int size)
        {
            this.total = total;
            this.size = size;
        }

        public void InitBuffer()
        {
            this.buffer = new byte[this.total];
        }

        public bool SetBufferr(SocketAsyncEventArgs args)
        {
            if (this.freeIndexPool.Count > 0)
            {
                args.SetBuffer(this.buffer, this.freeIndexPool.Pop(), this.size);
                return true;
            }

            if ((this.total - this.size) < this.current) return false;
            args.SetBuffer(this.buffer, this.current, this.size);
            this.current += this.size;
            return true;
        }


        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            this.freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
