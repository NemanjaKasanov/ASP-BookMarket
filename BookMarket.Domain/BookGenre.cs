using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class BookGenre
    {
        public int GenreId { get; set; }
        public int BookId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Book Book { get; set; }
    }
}
