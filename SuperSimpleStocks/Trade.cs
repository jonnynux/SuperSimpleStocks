﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class Trade
    {
        public enum Indicators { BUY, SELL };

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
            }
        }

        private uint _quantity;
        public uint Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value <= 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_006, value, "Quantity");
                _quantity = value;
            }
        }

        private string _indicator;
        public string Indicator
        {
            get
            {
                return _indicator;
            }
            set
            {
                if (Enum.IsDefined(typeof(Indicators), value))
                    _indicator = value;
                else
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_002, value,
                        string.Join(",", Enum.GetNames(typeof(Indicators))));
            }
        }

        private double _price;
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value <= 0)
                    throw new SuperSimpleStocksException(Properties.Resources.SUPERSIMPLESTOCKS_ERR_006, value, "Quantity");
                _price = value;
            }
        }

        private Stock _stock;
        public Stock Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                _stock = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Timestamp [{0}], Stock [{1}], Quantity [{2}], Indicator [{3}], Price [{4}]",
                _timestamp, _stock.StockSymbol, _quantity, _indicator, _price);
        }
    }
}