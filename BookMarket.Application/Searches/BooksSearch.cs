using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Searches
{
    public class BooksSearch : PagedSearch
    {
        public string Search { get; set; }

        public int? GenreId { get; set; }
        public int? PublisherId { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}
