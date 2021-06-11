using BookMarket.Application.DataTransfer;
using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.PublisherCommands
{
    public interface IUpdatePublisherCommand : ICommand<UpdatePublisherDto>
    {
    }
}
