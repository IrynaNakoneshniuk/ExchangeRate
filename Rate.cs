using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com


namespace ExchangeRateServerTCP
{
    public class Rate
    {
       public Dictionary<string,double> Rates=new Dictionary<string, double>() { {"USD", 36.5686 },
           {"EUR",40.1724},{"JPY",2.848 },{"PLN",8.5397},{"AUD",26.0734} };

    }
}
