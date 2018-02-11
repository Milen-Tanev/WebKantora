using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebKantora.Data.Models;

namespace WebKantora.Data
{
    public class WebKantoraDbContext : IdentityDbContext<User>
    {
        public WebKantoraDbContext(DbContextOptions<WebKantoraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Keyword> Keywords { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>()
                .HasMany<Keyword>(a => a.Keywords);
            builder.Entity<Keyword>()
                .HasMany<Article>(k => k.Articles);

            base.OnModelCreating(builder);
        }
    }
}
