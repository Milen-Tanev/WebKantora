using System;
using System.Collections.Generic;
using System.Linq;
using WebKantora.Data.Models;

namespace WebKantora.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebKantoraDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Articles.Any() && context.Keywords.Any())
            {
                return;
            }

            var user = context.Users.FirstOrDefault();

            var keywords = new List<Keyword>();
            for (int i = 0; i < 5; i++)
            {
                var keyWord = new Keyword()
                {
                    Content = $"keyword{i}"
                };
                keywords.Add(keyWord);
                context.Keywords.Add(keyWord);
                context.SaveChanges();
            }

            var articles = new List<Article>();

            for (int i = 0; i < 5; i++)
            {
                var article = new Article()
                {
                    Author = user,
                    Content = $"Content{i}",
                    Date = DateTime.Now
                };
                articles.Add(article);
            }
            
            foreach (var article in articles)
            {
                foreach (var keyword in keywords)
                {
                    article.Keywords.Add(keyword);
                }
                context.Articles.Add(article);
                context.SaveChanges();
            }
        }
    }
}
