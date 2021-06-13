using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class OrderBook
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}
