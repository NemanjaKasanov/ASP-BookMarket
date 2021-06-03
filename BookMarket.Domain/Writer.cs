using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Domain
{
    public class Writer : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
