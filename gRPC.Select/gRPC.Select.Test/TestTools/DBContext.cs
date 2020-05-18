using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace gRPC.Select.Test.TestTools
{
    public class DBContext : DbContext
    {
        protected DBContext()
        {
        }

        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DataModel> Table { get; set; }
    }
}
