using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.OrderCommands
{
    public interface IDeleteOrderCommand : ICommand<int>
    {
    }
}
