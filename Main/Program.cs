using CsvHelper;
using SuperSimpleStocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Read Stock Sample Data
                string filePath = "./resources/data/stocks_sample_data.csv";
                CsvReader csvR = new CsvReader(new StreamReader(filePath));
                csvR.Configuration.RegisterClassMap<StockCsvMap>();
                TradeOperations.Stocks = csvR.GetRecords<Stock>().ToList();
                csvR.Dispose();

                Console.WriteLine("** Stock Sample Data **");
                foreach (var stock in TradeOperations.Stocks)
                    Console.WriteLine(stock);
                Console.WriteLine();

                // Create a pseudo-rand generator to generate trades
                Random rand = new Random(117);

                // Create a DateTime
                DateTime date = new DateTime(2016, 01, 01, 13, 00, 00, 00);

                // Get Indicators
                Array indicators = Enum.GetValues(typeof(Trade.Indicators));

                // Initialize TradeOperations
                ITradeOperations tradeOperations = new TradeOperations();
                // Create 1000 trade operations
                Console.WriteLine("** Trade Operations **");
                for (int i = 0; i < 1000; i++)
                {
                    date = date.AddSeconds(rand.Next(1, 120));
                    Trade trade = new Trade
                    {
                        Indicator = indicators.GetValue(rand.Next(0, indicators.Length)).ToString(),
                        Price = rand.Next(1, 10000) / 100.0,
                        Quantity = (uint)rand.Next(1, 1000),
                        Stock = TradeOperations.Stocks.ElementAt(rand.Next(0, TradeOperations.Stocks.Count)),
                        Timestamp = date
                    };
                    trade.Stock.TickerPrice = trade.Price;
                    Console.WriteLine(string.Format("Trade [{0}]: {1}; Dividend Yeld [{2:0.##}], P/E Ratio [{3:0.##}]",
                        i + 1, trade, trade.Stock.DividendYeld(), trade.Stock.PERatio()));

                    tradeOperations.RecordTrade(trade);

                    // Print every 10 trades Stock Price and GBCE index
                    if ((i + 1) % 10 == 0)
                    {
                        StringBuilder sb = new StringBuilder("\tStock Prices: ");
                        foreach (Stock stock in TradeOperations.Stocks)
                            sb.Append(string.Format("[{0} = {1:0.##}]\t", stock.StockSymbol, tradeOperations.StockPrice(stock.StockSymbol)));
                        Console.WriteLine(sb);
                        Console.WriteLine(string.Format("\tGBCE All Share Index = [{0:0.##}]", tradeOperations.StocksGeometricMean()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
