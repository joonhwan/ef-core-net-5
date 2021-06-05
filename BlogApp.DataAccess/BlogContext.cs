using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}