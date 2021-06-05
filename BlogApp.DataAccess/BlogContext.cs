using System;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess
{
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = "User Id=BLOGDB;Password=BLOGUSER;Data Source=localhost:1521/XE";
            optionsBuilder.UseOracle(connectionString, builder =>
            {
                builder.UseOracleSQLCompatibility("11");
            });
            optionsBuilder.EnableSensitiveDataLogging(); // DBParameter의 값이 로그에 출력된다.
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); // 로그 출력을 위해 Console.WriteLine 을 사용
        }
    }
}