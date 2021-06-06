using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.Application.Interfaces
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Identity { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
