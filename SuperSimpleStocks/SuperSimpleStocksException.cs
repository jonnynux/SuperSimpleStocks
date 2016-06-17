using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    class SuperSimpleStocksException : Exception
    {
        public SuperSimpleStocksException() : base() { }
        public SuperSimpleStocksException(string message) : base(message) { }
        public SuperSimpleStocksException(string message, Exception innerException) : base(message, innerException) { }
        public SuperSimpleStocksException(string format, params object[] args)
            : base(string.Format(format, args)) { }
        public SuperSimpleStocksException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected SuperSimpleStocksException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
