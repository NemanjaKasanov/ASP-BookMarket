using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.WriterCommands
{
    public interface IUpdateWriterCommand : ICommand<UpdateWriterDto>
    {
    }
}
