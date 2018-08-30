using System;
using System.Collections.Generic;
using System.Text;

namespace Epic
{




    public static class Net
    {
        public static void CreateServer()
        {

        }


        public class ServerOption
        {
            public int Port { get; set; }
            public string Host { get; set; }
            public string Path { get; set; }
            public int Backlog { get; set; }
            public bool Exclusive { get; set; }
            public bool ReadableAll { get; set; }
            public bool WritableAll { get; set; }
        }

        public class Server
        {
            internal Server(string options, Action<Server> connectionListener)
            {

            }

            public void Listen(ServerOption options, Action action = null)
            {

            }

            public void Listen(int port, Action action = null)
            {

            }

        }

    }



}
