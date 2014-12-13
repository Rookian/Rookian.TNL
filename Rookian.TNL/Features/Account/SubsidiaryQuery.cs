using System.Collections.Generic;
using Rookian.TNL.Infrastructure.Handler;

namespace Rookian.TNL.Features.Account
{
    public class SubsidiaryQuery : IQuery<IEnumerable<Subsidiary>>
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public bool? IsPublic { get; set; }
    }
}