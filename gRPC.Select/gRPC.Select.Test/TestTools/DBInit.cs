using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;
using NSubstitute;

namespace gRPC.Select.Test.TestTools
{
    public class DBInit
    {
        public DBInit(string testName)
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DbContextOptions = CreateDbOptions(testName);
        }

        public IConfiguration Configuration { get; }

        public DbContextOptions<DBContext> DbContextOptions { get; }

        private DbContextOptions<DBContext> CreateDbOptions(string testName)
        {
            string _connectionString = Configuration.GetConnectionString("DefaultConnection");
            NpgsqlConnectionStringBuilder _stringBuilder = new NpgsqlConnectionStringBuilder(_connectionString);
            _stringBuilder.Database = _stringBuilder.Database + "_" + testName;

            var _ping = new Ping();
            var _reply = _ping.Send(_stringBuilder.Host, 5 * 1000);
            if (_reply.Status != IPStatus.Success)
            {
                throw new Exception("PGSQL Not Avaliable");
            }

            return new DbContextOptionsBuilder<DBContext>()
                .UseNpgsql(_stringBuilder.ToString()).Options;
        }

        public void InitDB()
        {
            using DBContext _context = new DBContext(DbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            AddValues();
        }

        public void CleanupDB()
        {
            using (DBContext _context = new DBContext(DbContextOptions))
            {
                _context.Database.EnsureDeleted();
            }
        }

        private void AddValues()
        {
            using DBContext _context = new DBContext(DbContextOptions);
            _context.Table.Add(new DataModel()
                {StringValue = "1", DoubleValue = 1.1, FloatValue = (float) 1.2, BoolValue = false});
            _context.Table.Add(new DataModel()
                {StringValue = "2", DoubleValue = 2.1, FloatValue = (float) 2.2, BoolValue = true});
            _context.Table.Add(new DataModel()
                {StringValue = "3", DoubleValue = 3.1, FloatValue = (float) 3.2, BoolValue = false});
            _context.Table.Add(new DataModel()
                {StringValue = "4", DoubleValue = 4.1, FloatValue = (float) 4.2, BoolValue = true});
            _context.Table.Add(new DataModel()
                {StringValue = "aOeUi", DoubleValue = 5.1, FloatValue = (float) 5.2, BoolValue = false});
            _context.SaveChanges();
        }
    }
}
