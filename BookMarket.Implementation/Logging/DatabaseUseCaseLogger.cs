using BookMarket.Application.Interfaces;
using BookMarket.DataAccess;
using BookMarket.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly BookMarketContext context;
        public DatabaseUseCaseLogger(BookMarketContext context)
        {
            this.context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor applicationActor, object useCaseData)
        {
            context.UseCaseLogs.Add(new UseCaseLog
            {
                Actor = applicationActor.Identity,
                Data = JsonConvert.SerializeObject(useCaseData),
                Date = DateTime.UtcNow,
                UseCaseName = useCase.Name
            });

            context.SaveChanges();
        }
    }
}
