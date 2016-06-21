using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class TradeOperations : ITradeOperations
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Stock> Stocks = new List<Stock>();

        public static List<Trade> Trades = new List<Trade>();

        public bool RecordTrade(Trade trade)
        {
            try
            {
                Trades.Add(trade);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(Properties.Resources.SUPERSIMPLESTOCKS_ERR_004, trade, ex.Message);
                return false;
            }
            return true;
        }

        public double StocksGeometricMean()
        {
            List<Stock> validStocks = new List<Stock>();
            validStocks.AddRange(Stocks.Where(x => StockPrice(x.StockSymbol) > 0));
            return Math.Pow(validStocks.Aggregate(1.0, (tot, x) => tot * StockPrice(x.StockSymbol)), 1.0 / validStocks.Count());
        }

        public double StockPrice(string stockSymbol)
        {
            List<Trade> validTrades = new List<Trade>();
            validTrades.AddRange(Trades.Where(x => x.Stock.StockSymbol == stockSymbol && x.Timestamp >
                Trades.ElementAt(Trades.Count - 1).Timestamp.Subtract(new TimeSpan(0, 15, 0))));

            if (validTrades.Count() <= 0)
            {
                Log.WarnFormat(Properties.Resources.SUPERSIMPLESTOCKS_ERR_003, stockSymbol);
                return 0;
            }

            return validTrades.Aggregate(0.0, (tot, x) => tot + x.Price * x.Quantity) /
                validTrades.Aggregate(0.0, (tot, x) => tot + x.Quantity);
        }
    }
}
