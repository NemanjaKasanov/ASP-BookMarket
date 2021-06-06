using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.UserCommands
{
    public interface IDeleteUserCommand : ICommand<int>
    {
    }
}
