using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Searches
{
    public class CartsSearch : PagedSearch
    {
        public int? UserId { get; set; }
        public int? BookId { get; set; }
    }
}
