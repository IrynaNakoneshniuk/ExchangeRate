using ExchangeRateServerTCP;
using Server_TCP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com


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
            try
            {
                ServerConnection.StartServer();
                ServerConnection.MaxAmountConnections = Convert.ToInt32(mainMV.Selected_qtyConnection);
                ServerConnection.AmountQueries = Convert.ToInt32(mainMV.Selected_qtyQueries);
                while (mainMV.IsCheked)
                {
                    await Task.Run(async () => await ServerConnection.WriteMsgAsync());
                  
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ServerConnection.ServerStop();
            }
        }
    }
}
