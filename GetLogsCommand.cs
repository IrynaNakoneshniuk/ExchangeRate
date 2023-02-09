using Server_TCP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExchangeRate
{
    public class GetLogsCommand : ICommand
    {
        private MainMV mainMV;
        public GetLogsCommand(MainMV mainMV)
        {
            this.mainMV = mainMV;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            while (mainMV.IsCheked)
            {
                await Task.Run(() => mainMV.RemoteEPAdress = ServerConnection.GetLogsOfConnection());
            }
        }
    }
}
