using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.DataTransfer
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public WriterDto Writer { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public PublisherDto Publisher { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; } = new List<GenreDto>();
    }
}
