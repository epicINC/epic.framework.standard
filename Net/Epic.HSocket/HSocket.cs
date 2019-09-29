using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Epic.Net
{
    public class HSocket : IDisposable
    {
        const int BufferSize = 1024;
        const int opsToPreAlloc = 2;

        Socket Socket;
        BufferManager Buffer;
        Stack<SocketAsyncEventArgs> Pool = new Stack<SocketAsyncEventArgs>();
        Semaphore MaxAcceptedClients;

        IPAddress Address;
        int Port;
        int MaxClients;
        bool IsRunning;

        public HSocket(IPAddress address, int port, int maxClients)
        {
            this.Socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.Buffer = new BufferManager(maxClients * BufferSize * opsToPreAlloc, BufferSize);
            this.MaxClients = maxClients;
            this.Port = port;
            this.Address = address;
            this.MaxAcceptedClients = new Semaphore(maxClients, maxClients);
        }



        void Init()
        {
            
            SocketAsyncEventArgs item;
            for (var i = 0; i < this.MaxClients; i++)
            {
                item = new SocketAsyncEventArgs();
                item.Completed += Item_Completed;
                item.UserToken = null;
                this.Buffer.SetBufferr(item);
                this.Pool.Push(item);
            }
        }

        void Item_Completed(object sender, SocketAsyncEventArgs e)
        {
        }

        public void Start()
        {
            if (this.IsRunning) return;
            this.IsRunning = true;
            this.Init();
            var endPoint = new IPEndPoint(this.Address, this.Port);
            this.Socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            if (this.Address.AddressFamily == AddressFamily.InterNetworkV6)
            {
                this.Socket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                this.Socket.Bind(new IPEndPoint(IPAddress.IPv6Any, this.Port));
            }
            else
                this.Socket.Bind(endPoint);

            this.Socket.Listen(this.MaxClients);

            StartAccept(null);

        }

        public void Stop()
        {
            if (!this.IsRunning) return;
            this.Socket.Dispose();
            this.IsRunning = false;
        }

        void StartAccept(SocketAsyncEventArgs e)
        {
            if (e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += OnCompletedAccepted;
            }
            else
                e.AcceptSocket = null;
            this.MaxAcceptedClients.WaitOne();
            if (this.Socket.AcceptAsync(e)) return;
            ProcessAccept(e);
        }

        void OnCompletedAccepted(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessAccept(e);
        }

        int ClientCount;

        void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success) return;
            var socket = e.AcceptSocket;
            if (!socket.Connected) return;
            Interlocked.Increment(ref this.ClientCount);
            var item = this.Pool.Pop();
            item.UserToken = socket;

            if (socket.ReceiveAsync(item)) return;
            this.ProcessAccept(item);
        }


        public void Send(SocketAsyncEventArgs e, byte[] data)
        {
            if (e.SocketError != SocketError.Success) return;
            var socket = e.AcceptSocket;
            if (!socket.Connected) return;

            Array.Copy(data, 0, e.Buffer, 0, data.Length);
            if (socket.SendAsync(e))
                this.ProcessSend(e);
            else
                this.CloseClient(e);

        }

        void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {

            }
            else
            {
                this.CloseClient(e);
            }
        }

        void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                this.CloseClient(e);
                return;
            }

            if (e.BytesTransferred < 1) return;
            var socket = (Socket)e.UserToken;
            if (socket.Available == 0)
            {
                var data = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                
            }

            if (socket.ReceiveAsync(e))
                this.ProcessReceive(e);
        }


        void OnIOCompleted(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    this.ProcessAccept(e);
                    break;
                case SocketAsyncOperation.Connect:
                    break;
                case SocketAsyncOperation.Disconnect:
                    break;
                case SocketAsyncOperation.None:
                    break;
                case SocketAsyncOperation.Receive:
                    this.ProcessReceive(e);
                    break;
                case SocketAsyncOperation.ReceiveFrom:
                    break;
                case SocketAsyncOperation.ReceiveMessageFrom:
                    break;
                case SocketAsyncOperation.Send:
                    break;
                case SocketAsyncOperation.SendPackets:
                    break;
                case SocketAsyncOperation.SendTo:
                    break;
                default:
                    break;
            }
        }

        void CloseClient(SocketAsyncEventArgs e)
        {
            this.CloseClient(e.UserToken as Socket, e);
        }

        void CloseClient(Socket socket, SocketAsyncEventArgs e)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Send);
            }
            finally
            {
                socket.Close();
            }

            Interlocked.Decrement(ref this.ClientCount);
            this.MaxAcceptedClients.Release();
            this.Pool.Push(e);
        }



        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed) return;


        }
    }
}
