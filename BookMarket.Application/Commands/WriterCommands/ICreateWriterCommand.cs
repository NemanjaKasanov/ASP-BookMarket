﻿using BookMarket.Application.Interfaces;
using BookMarket.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Commands.WriterCommands
{
    public interface ICreateWriterCommand : ICommand<Writer>
    {
    }
}
