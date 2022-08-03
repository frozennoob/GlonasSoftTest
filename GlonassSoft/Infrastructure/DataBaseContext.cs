using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace GlonassSoft.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }

        public string DbPath { get; set; }
        
        public DataBaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "queries.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
}
