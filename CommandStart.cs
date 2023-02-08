using ExchangeRateServerTCP;
using Server_TCP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TCP_ServerLib;

namespace ExchangeRate
{
    public class CommandStart : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private MainMV mainMV;
        public CommandStart(MainMV mainMV)
        {
            this.mainMV = mainMV;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            int count = 0;
            ServerConnection.AmountConnections = Convert.ToInt32(mainMV.Selected_qtyConnection);
            ServerConnection.StartServer();
            while (true)
            {
               await Task.Run(async () =>
                {
                    Rate rate = new Rate();
                    TcpClient tcpClient = await ServerConnection.GetClientAsync();
                    NetworkStream network = tcpClient.GetStream();
                    string[] res = await ServerConnection.ReadMsgAsync(network);
                    var map = (from i in Authentification.SinIn
                               where i.Key == res[0] && i.Value == res[1]
                               select i).FirstOrDefault();
                    if (map.Value != null)
                    {
                        while (mainMV.IsCheked)
                        {
                            string[] Currency = await ServerConnection.ReadMsgAsync(network);
                            double rates = (from i in rate.Rates
                                            where i.Key == Currency[0]
                                            select i.Value).FirstOrDefault();
                            ServerConnection.WriteMsgAsync(rate.ToString(), Convert.ToInt32(mainMV.Selected_qtyQueries), count);
                            count++;
                        }
                    }
                    else
                    {
                        await ServerConnection.WriteErrAuthent(network, tcpClient);
                    }
                });
                await Task.Run(() =>
                {
                    mainMV.RemoteEPAdress += ServerConnection.GetLogsOfConnection();
                    mainMV.RemoteEPAdress += "\n";
                });
            }
        }
    }
}
