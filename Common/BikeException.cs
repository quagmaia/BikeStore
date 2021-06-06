using System;
using System.Collections.Generic;
using System.Text;

namespace BikeCommon
{
    public class BikeException : Exception
    {
        public BikeException(string message) : base(message)
        {

        }
    }
}
