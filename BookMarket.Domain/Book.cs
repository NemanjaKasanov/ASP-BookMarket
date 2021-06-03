using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }

        public int WriterId { get; set; }
        public int PublisherId { get; set; }

        public virtual Writer Writer { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; } = new HashSet<BookGenre>();
    }
}
