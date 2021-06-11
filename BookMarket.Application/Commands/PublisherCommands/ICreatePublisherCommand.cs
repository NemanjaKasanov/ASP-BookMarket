﻿using BookMarket.Application.Interfaces;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.PublisherCommands
{
    public interface ICreatePublisherCommand : ICommand<Publisher>
    {
    }
}
