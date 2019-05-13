using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.IPAddress l_ip =
                System.Net.IPAddress.Parse("127.0.0.1");

            int l_port = 2001;

            System.Net.Sockets.TcpListener l_listener =
                new System.Net.Sockets.TcpListener(l_ip, l_port);

            l_listener.Start();

            Console.WriteLine("Listenを開始しました({0} : {1})",
                ((System.Net.IPEndPoint)l_listener.LocalEndpoint).Address,
                ((System.Net.IPEndPoint)l_listener.LocalEndpoint).Port);

            System.Net.Sockets.TcpClient l_client =
                l_listener.AcceptTcpClient();

            Console.WriteLine("client({0} : {1})と接続しました",
                ((System.Net.IPEndPoint)l_client.Client.RemoteEndPoint).Address,
                ((System.Net.IPEndPoint)l_client.Client.RemoteEndPoint).Port);

            System.Net.Sockets.NetworkStream l_ns = l_client.GetStream();

            l_ns.ReadTimeout = 10000;
            l_ns.WriteTimeout = 10000;

            System.Text.Encoding l_enc = System.Text.Encoding.UTF8;

            bool l_disconnected = false;

            System.IO.MemoryStream l_ms = new System.IO.MemoryStream();

            byte[] l_resBytes = new byte[256];
            int l_resSize = 0;

            do
            {
                l_resSize = l_ns.Read(l_resBytes, 0, l_resBytes.Length);
                if(l_resSize == 0)
                {
                    l_disconnected = true;
                    Console.WriteLine("clientが切断しました");
                    break;
                }
                l_ms.Write(l_resBytes, 0, l_resSize);
            } while (l_ns.DataAvailable || l_resBytes[l_resSize -1] != '\n');

            string l_resMsg = l_enc.GetString(l_ms.GetBuffer(), 0,(int)l_ms.Length);
            l_ms.Close();

            l_resMsg = l_resMsg.TrimEnd('\n');
            Console.WriteLine(l_resMsg);

            if (!l_disconnected)
            {
                string l_sendMsg = l_resMsg.Length.ToString();
                byte[] l_sendBytes = l_enc.GetBytes(l_sendMsg + '\n');
                Console.WriteLine(l_sendMsg);
            }

            l_ns.Close();
            l_client.Close();
            Console.WriteLine("clientとの接続を閉じました");

            l_listener.Stop();
            Console.WriteLine("Listenerを閉じました。");

            Console.ReadLine();
        }
    }
}
