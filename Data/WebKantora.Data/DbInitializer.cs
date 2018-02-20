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
            }

            context.AddRange(keywords);

            var articles = new List<Article>();

            for (int i = 0; i < 5; i++)
            {
                var article = new Article()
                {
                    Title = $"Article{i}",
                    Author = user,
                    Content = @"Lorem ipsum dolor sit amet, 
                                consectetur adipiscing elit, sed do eiusmod 
                                tempor incididunt ut labore et dolore magna aliqua. 
                                Ut enim ad minim veniam, quis nostrud exercitation 
                                ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                                Duis aute irure dolor in reprehenderit in voluptate velit 
                                esse cillum dolore eu fugiat nulla pariatur. 
                                Excepteur sint occaecat cupidatat non proident, 
                                sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.Now
                };
                articles.Add(article);
            }

            context.AddRange(articles);
            context.SaveChanges();

            var kas = new List<KeywordArticle>();
            var random = new Random();

            foreach (var article in articles)
            {
                var ka = new KeywordArticle()
                {
                    Article = article,
                    Keyword = keywords[random.Next(0, 4)]
                };
                kas.Add(ka);
            }

            context.AddRange(kas);
            context.SaveChanges();
        }
    }
}
