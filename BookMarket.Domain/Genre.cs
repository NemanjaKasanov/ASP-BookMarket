using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; } = new HashSet<BookGenre>();
    }
}
