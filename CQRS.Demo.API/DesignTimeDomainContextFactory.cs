using CQRS.Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CQRS.Demo.API
{
    public class DesignTimeDomainContextFactory : IDesignTimeDbContextFactory<DomainContext>
    {
        public DomainContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DomainContext>();
            optionsBuilder.UseSqlServer("data source=.;initial catalog=CQRS.Demo;integrated security=True;multipleactiveresultsets=True;App=EntityFramework");

            return new DomainContext(optionsBuilder.Options);
        }
    }
}