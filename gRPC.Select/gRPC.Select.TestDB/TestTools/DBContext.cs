using Microsoft.EntityFrameworkCore;

namespace gRPC.Select.TestDB.TestTools
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
