using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public DateTime Time { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; }
    }
}
