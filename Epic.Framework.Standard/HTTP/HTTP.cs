using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using static Epic.Net;

namespace Epic
{
    public static class HTTP
    {
        public static HttpServer CreateServer(Action<HttpListenerRequest, HttpListenerResponse> onRequest)
        {
            return new HttpServer(onRequest);
        }


    }


    public class HttpServer
    {

        HttpListener HttpListener { get; set; }
        Action<HttpListenerRequest, HttpListenerResponse> OnRequest;

        public HttpServer(Action<HttpListenerRequest, HttpListenerResponse> onRequest)
        {
            
            this.OnRequest = onRequest;
        }

        void OnGetContext(IAsyncResult ar)
        {
            var context = this.HttpListener.EndGetContext(ar);
            this.HttpListener.BeginGetContext(new AsyncCallback(OnGetContext), this.HttpListener);
            this.OnRequest(context.Request, context.Response);

        }

        public void Listen(ServerOption options, Action action = null)
        {
            this.HttpListener = new HttpListener();
            this.HttpListener.Prefixes.Add($"http://{options.Host ?? "+"}:{options.Port}");
            this.HttpListener.BeginGetContext(new AsyncCallback(OnGetContext), this.HttpListener);
            this.HttpListener.Start();

            if (action == null) return;
            
            action();
        }

        public void Listen(int port, string host = null)
        {

        }

        public void On(string name, Action<HttpListenerRequest, HttpListenerWebSocketContext, string> action)
        {

        }

    }
}
