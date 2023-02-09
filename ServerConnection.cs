using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Security;
using ExchangeRateServerTCP;
using System.Threading;

namespace Server_TCP2
{
    public static class ServerConnection
    {
        public static TcpListener server ;
        public static int MaxAmountConnections;
        public static int AmountQueries;
        private static IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        private static TcpConnectionInformation[] tcpConnections =ipProperties.GetActiveTcpConnections();
        
        public static async Task<string[]> ReadMsgAsync(NetworkStream ns)
        {
            byte[] bufRead = new byte[1024];
            string[] res = null;
            int length = await ns.ReadAsync(bufRead);
            List<byte> tmp = new List<byte>();
            for (int i = 0; i < length; i++)
            {
                tmp.Add(bufRead[i]);
            }
            res = Encoding.Unicode.GetString(tmp.ToArray()).Split(' ');
            return res;
        }
       
        public static void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 8080);
            server.Start();
        }
        public static async Task WriteMsgAsync()
        {
            Rate rate1 = new Rate();
            int count = 0;
            string[] arrRes = new string[2];
            string Currency = null;
            TcpClient clt = await server.AcceptTcpClientAsync();
            if (clt!=null)
            {
                NetworkStream ns = clt.GetStream();
                try
                {
                    arrRes = await ReadMsgAsync(ns);
                    string resAuthent = (from i in Authentification.SinIn.AsParallel()
                                         where i.Key == arrRes[0] && i.Value == arrRes[1]
                                         select i.Value).FirstOrDefault();
                    if (resAuthent != null)
                    {
                        if (tcpConnections.Length < MaxAmountConnections)
                        {
                            while (count < AmountQueries)
                            {
                                string[] GetMsg = await ReadMsgAsync(ns);

                                double rate = (from i in rate1.Rates
                                               where i.Key == GetMsg[0]
                                               select i.Value).FirstOrDefault();
                                byte[] buf = Encoding.Unicode.GetBytes(rate.ToString());
                                await ns.WriteAsync(buf);
                                count++;
                            }
                            await ns.WriteAsync(Encoding.Unicode.GetBytes("Request is failed, overlimit of requests"));
                            clt.Close();
                            Thread.Sleep(60000);
                        }
                        else
                        {
                            await ns.WriteAsync(Encoding.Unicode.GetBytes("Request is failed, overlimit of requests"));
                            clt.Close();
                        }
                    }
                    else
                    {
                        await WriteErrAuthent(ns, clt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    ns.Close();
                }
            }
           
        }

        public static async Task WriteErrAuthent(NetworkStream ns, TcpClient clt)
        {
            try
            {
                byte[] bufWrite = Encoding.Unicode.GetBytes("Request is failed, uncorrect password or login!");
                await ns.WriteAsync(bufWrite);
                clt.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string GetLogsOfConnection()
        {
            string log=null;
            foreach (TcpConnectionInformation info in tcpConnections)
            {
                log += info.RemoteEndPoint;
                log += " ";
                log += info.State;
                log += " ";
            }
            return log;
        }
        public static void ServerStop()
        {
            server.Stop();
        }
    }
}
