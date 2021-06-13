using BookMarket.Application.Interfaces;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookMarket.Application.Commands.CartCommands
{
    public interface ICreateCartCommand : ICommand<Cart>
    {
    }
}
