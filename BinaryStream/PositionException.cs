using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStream
{
    public class PositionException : Exception
    {
        public PositionException() { }
        public PositionException(string message) : base(message) { }
        public PositionException(string message, Exception inner) : base(message, inner) { }
    }
}
