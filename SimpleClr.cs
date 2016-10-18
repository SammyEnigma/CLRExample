using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Text;

namespace SimpleClr
{
    public class SimpleClr
    {

        private static readonly TcpListener Server = createServer(8002);

        private static readonly List<TcpClient> clients;

        [SqlProcedure]
        public static void BroadcastXml(SqlXml xml)
        {
            var data = Encoding.Default.GetBytes(xml.Value);

            foreach (var client in clients)
            {
                client.GetStream().Write(data, 0, data.Length);
            }
        }

        private static TcpListener createServer(int port)
        {
            var listener = new TcpListener(System.Net.IPAddress.Any, port);

            listener.BeginAcceptTcpClient((ar) =>
            {
                var res = listener.EndAcceptTcpClient(ar);
                clients.Add(res);
            }, null);

            return listener;
        }

    }
}