using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException(string message) : base(message)
        {
        }
    }
}
