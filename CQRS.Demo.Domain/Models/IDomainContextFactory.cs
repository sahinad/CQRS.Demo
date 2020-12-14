using Microsoft.EntityFrameworkCore;
using System;

namespace CQRS.Demo.Domain.Models
{
    public interface IDomainContextFactory : IDbContextFactory<DomainContext>, IDisposable
    {
    }
}
