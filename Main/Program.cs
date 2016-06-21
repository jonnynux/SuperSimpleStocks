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

                // Print sample data
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
                // Create 1000 trade operations with randoms
                Console.WriteLine("** Trade Operations **");
                for (int i = 0; i < 1000; i++)
                {
                    // Add random seconds to a date
                    date = date.AddSeconds(rand.Next(1, 120));
                    // Create a trade with randoms
                    Trade trade = new Trade
                    {
                        Indicator = indicators.GetValue(rand.Next(0, indicators.Length)).ToString(),
                        Price = rand.Next(1, 10000) / 100.0,
                        Quantity = rand.Next(1, 1000),
                        Stock = TradeOperations.Stocks.ElementAt(rand.Next(0, TradeOperations.Stocks.Count)),
                        Timestamp = date
                    };

                    // Print the trade with calculated Dividend Yeld and P/E Ratio
                    Console.WriteLine(string.Format("Trade [{0}]: {1}; Dividend Yeld [{2:0.##}], P/E Ratio [{3:0.##}]",
                        i + 1, trade, trade.DividendYeld(), trade.PERatio()));

                    // Record the trade in the list of trades
                    tradeOperations.RecordTrade(trade);

                    // Print Stock Prices
                    StringBuilder sb = new StringBuilder("\tStock Prices: ");
                    foreach (Stock stock in TradeOperations.Stocks)
                        sb.Append(string.Format("[{0} = {1:0.##}]\t", stock.StockSymbol, tradeOperations.StockPrice(stock.StockSymbol)));
                    Console.WriteLine(sb);

                    // Print GBCE All Share Index
                    Console.WriteLine(string.Format("\tGBCE All Share Index = [{0:0.##}]", tradeOperations.StocksGeometricMean()));
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
