using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebKantora.Data.Models;

namespace WebKantora.Data
{
    public class WebKantoraDbContext : IdentityDbContext<User, Role, Guid>
    {
        public WebKantoraDbContext(DbContextOptions<WebKantoraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<CustomError> CustomErrors { get; set; }

        public DbSet<Keyword> Keywords { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<KeywordArticle>()
                .HasKey(ka => new { ka.KeywordId, ka.ArticleId });

            builder.Entity<KeywordArticle>()
                .HasOne(ka => ka.Keyword)
                .WithMany(k => k.KeywordArticles)
                .HasForeignKey(ka => ka.KeywordId);

            builder.Entity<KeywordArticle>()
                .HasOne(ka => ka.Article)
                .WithMany(a => a.KeywordArticles)
                .HasForeignKey(ka => ka.ArticleId);

            base.OnModelCreating(builder);
        }
    }
}
