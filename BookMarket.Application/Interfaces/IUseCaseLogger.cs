using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Interfaces
{
    public interface IUseCaseLogger
    {
        void Log(IUseCase useCase, IApplicationActor applicationActor, object useCaseData);
    }
}
