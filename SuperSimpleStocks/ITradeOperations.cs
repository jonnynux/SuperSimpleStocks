using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public interface ITradeOperations
    {
        bool RecordTrade(Trade trade);

        double StocksGeometricMean();

        double StockPrice(string stockSymbol);
    }
}
