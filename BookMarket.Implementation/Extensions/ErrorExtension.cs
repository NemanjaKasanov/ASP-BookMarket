using BookMarket.Application;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMarket.Implementation.Extensions
{
    public static class ErrorExtension
    {
        public static UnprocessableEntityObjectResult AsUnprocessabeEntity(this ValidationResult result)
        {
            var errorMessages = result.Errors.Select(x => new ClientError
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName
            });

            return new UnprocessableEntityObjectResult(new
            {
                Errors = errorMessages
            });
        }
    }
}
