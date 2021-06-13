using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
