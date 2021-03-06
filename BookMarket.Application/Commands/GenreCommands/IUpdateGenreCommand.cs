using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.GenreCommands
{
    public interface IUpdateGenreCommand : ICommand<UpdateGenreDto>
    {
    }
}
