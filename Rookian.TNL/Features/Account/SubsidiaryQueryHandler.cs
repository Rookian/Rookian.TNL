using System.Collections.Generic;
using System.Linq;
using Rookian.TNL.Infrastructure.Handler;

namespace Rookian.TNL.Features.Account
{
    public class SubsidiaryQueryHandler : IQueryHandler<SubsidiaryQuery, IEnumerable<Subsidiary>>
    {
        private readonly IEnumerable<Subsidiary> Subsidiaries = new List<Subsidiary>()
        {
            new Subsidiary{ Id = 1, Name = "Sub1", IsPublic = true},
            new Subsidiary{ Id = 2, Name = "Sub2", IsPublic = true},
            new Subsidiary{ Id = 3, Name = "Sub3", IsPublic = false},
            new Subsidiary{ Id = 4, Name = "Sub4", IsPublic = true},
        };

        public IEnumerable<Subsidiary> Handle(SubsidiaryQuery query)
        {
            var subsidiaries = Subsidiaries;

            if (query.Id.HasValue)
                subsidiaries = subsidiaries.Where(x => x.Id == query.Id.Value);

            if (query.IsPublic.HasValue)
                subsidiaries = subsidiaries.Where(x => x.IsPublic == query.IsPublic);

            if (!string.IsNullOrEmpty(query.Name))
                subsidiaries = subsidiaries.Where(x => x.Name.Contains(query.Name));
            
            return subsidiaries;
        }
    }
}