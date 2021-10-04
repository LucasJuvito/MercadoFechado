using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ServidorTestes
{
    public class APIServer
    {
        private Dictionary<string, Action<HttpListenerContext, StreamWriter, StreamReader>> Handlers = new Dictionary<string, Action<HttpListenerContext, StreamWriter, StreamReader>>();
        private HttpListener Listener;
        private string BaseURL = "";

        public APIServer(int port)
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://+:" + port + "/");
            Console.WriteLine("Listening on port {0}", port);
        }

        public void SetBaseURL(string baseURL)
        {
            this.BaseURL = baseURL;
        }

        public void Listen()
        {
            Listener.Start();
            while (Listener.IsListening)
            {
                var context = Listener.GetContext();

                Task.Run(() =>
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(context.Response.OutputStream))
                        {
                            using (StreamReader reader = new StreamReader(context.Request.InputStream))
                            {
                                ProcessContext(context, writer, reader);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
        }

        private void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            Uri url = context.Request.Url;
            string handler = url.AbsolutePath;

            if (!Handlers.ContainsKey(handler))
            {
                writer.Write(new BaseResponse() { Message = "Invalid handler" }.ToJSON());
                return;
            }

            Handlers[handler].Invoke(context, writer, reader);
        }

        public void AddAction(string handler, Action<HttpListenerContext, StreamWriter, StreamReader> action)
        {
            Handlers.Add(BaseURL + handler, action);
        }
    }
}

