using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Email
{
    public interface IEmailSender
    {
        void Send(SendEmailDto dto);
    }
}
