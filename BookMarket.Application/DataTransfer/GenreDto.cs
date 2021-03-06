using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.DataTransfer
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
