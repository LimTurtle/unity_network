using System;
using System.Net;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace unity_websocket_testserver
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data;
            Console.WriteLine("Recv: " + msg);
            Console.WriteLine("Send: " + msg);
            Send(msg);
        }
    }
    public class Program
    {
        public static void Main(string[] args) 
        {
            var wssv = new WebSocketServer(IPAddress.Any, 6000);
            wssv.AddWebSocketService<Echo>("/Echo");

            try
            {
                wssv.Start();
                Console.WriteLine("WS server started on ws://localhost:6000/Echo");

                Console.ReadKey(true);
                wssv.Stop();
            }
            catch(Exception ex) { throw; }
        }
    }
}
