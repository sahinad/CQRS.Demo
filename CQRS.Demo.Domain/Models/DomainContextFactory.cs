using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace CQRS.Demo.Domain.Models
{
    public class DomainContextFactory : IDomainContextFactory
    {
        private readonly string _connectionString;
        private SqlConnection _sqlConnection;

        public DomainContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DomainContext CreateDbContext()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            return new DomainContext(GetDbContextOptions(_sqlConnection));
        }

        private DbContextOptions GetDbContextOptions(DbConnection dbConnection)
        {
            DbContextOptionsBuilder<DomainContext> builder = new DbContextOptionsBuilder<DomainContext>();
            builder.UseSqlServer(dbConnection);
            return builder.Options;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}
