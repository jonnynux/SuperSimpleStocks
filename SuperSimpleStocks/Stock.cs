using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class Stock
    {
        public enum Types { COMMON, PREFERRED };

        private string _stockSymbol;
        public string StockSymbol
        {
            get
            {
                return _stockSymbol;
            }
            set
            {
                _stockSymbol = value;
            }
        }

        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (Enum.IsDefined(typeof(Types), value))
                    _type = value;
                else
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_001, value,
                        string.Join(",", Enum.GetNames(typeof(Types))));
            }
        }

        private int _lastDividend;
        public int LastDividend
        {
            get
            {
                return _lastDividend;
            }
            set
            {
                if (value < 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_005, value, "Last Dividend");
                _lastDividend = value;
            }
        }


        private double _fixedDividend;
        public double FixedDividend
        {
            get
            {
                return _fixedDividend;
            }
            set
            {
                if (value < 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_005, value, "Fixed Dividend");
                _fixedDividend = value;
            }
        }

        private uint _parValue;
        public uint ParValue
        {
            get
            {
                return _parValue;
            }
            set
            {
                if (value < 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_005, value, "Par Value");
                _parValue = value;
            }
        }

        private double _tickerPrice;
        public double TickerPrice
        {
            get
            {
                return _tickerPrice;
            }
            set
            {
                if (value <= 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_006, value, "Ticker Price");
                _tickerPrice = value;
            }
        }

        public double DividendYeld()
        {
            return _type == Types.COMMON.ToString() ? _lastDividend / _tickerPrice :
                _type == Types.PREFERRED.ToString() ? _fixedDividend * _parValue / _tickerPrice : -1;
        }

        public double PERatio()
        {
            return _tickerPrice / DividendYeld();
        }

        public override string ToString()
        {
            return string.Format("StockSymbol [{0}], Type [{1}], LastDividend [{2}], FixedDividend [{3}], ParValue [{4}]",
                _stockSymbol, _type, _lastDividend, _fixedDividend, _parValue);
        }
    }
}
