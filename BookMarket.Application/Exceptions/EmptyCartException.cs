using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Exceptions
{
    public class EmptyCartException : Exception
    {
        public EmptyCartException()
            : base($"The cart of the given user is empty.")
        {

        }
    }
}
