using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateServerTCP
{
    public class Rate
    {
       public Dictionary<string,double> Rates=new Dictionary<string, double>() { {"USD", 36.5686 },
           {"EUR",40.1724},{"JPY",2.848 },{"PLN",8.5397},{"AUD",26.0734} };

    }
}
