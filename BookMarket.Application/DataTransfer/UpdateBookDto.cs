using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.DataTransfer
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int WriterId { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public int PublisherId { get; set; }
    }
}
