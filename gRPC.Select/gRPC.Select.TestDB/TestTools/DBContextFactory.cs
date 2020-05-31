using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace gRPC.Select.TestDB.TestTools
{
    public class DBContextFactory: IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .Build();
            string _connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new DBContext(new DbContextOptionsBuilder().UseNpgsql(_connectionString).Options);
        }
    }
}
