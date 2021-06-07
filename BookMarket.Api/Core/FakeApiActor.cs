using BookMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Api.Core
{
    public class FakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Test API User";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1 };
    }

    public class AdminFakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Test API Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 100);
    }
}
