using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Searches
{
    public class OrdersSearch : PagedSearch
    {
        public int? OrderId { get; set; }
        public int? UserId { get; set; }
    }
}
