using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.DataTransfer
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
    }
}
