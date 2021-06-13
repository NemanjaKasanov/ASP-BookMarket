using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.BookCommands
{
    public interface IDeleteBookCommand : ICommand<int>
    {
    }
}
