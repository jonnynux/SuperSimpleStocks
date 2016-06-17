using CsvHelper.Configuration;
using SuperSimpleStocks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public sealed class StockCsvMap : CsvClassMap<Stock>
    {
        public StockCsvMap()
        {
            Map(m => m.StockSymbol);
            Map(m => m.Type);
            Map(m => m.LastDividend);
            Map(m => m.FixedDividend);
            Map(m => m.ParValue);
        }
    }
}
