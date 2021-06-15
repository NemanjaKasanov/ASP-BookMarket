using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Searches
{
    public class CasesSearch : PagedSearch
    {
        public int? UserId { get; set; }
        public int? UseCaseId { get; set; }
    }
}
