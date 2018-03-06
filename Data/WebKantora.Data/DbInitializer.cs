using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebKantora.Data.Models;

namespace WebKantora.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebKantoraDbContext context, IApplicationBuilder app)
        {
            context.Database.EnsureCreated();

            if (context.Articles.Any() && context.Keywords.Any())
            {
                return;
            }

            var user = context.Users.FirstOrDefault();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var administrationRole = "Admin";
                var userRole = "User";

                var roles = new[]
                {
                    administrationRole,
                    userRole
                };

                foreach (var r in roles)
                {
                    var role = roleManager.CreateAsync(new IdentityRole
                    {
                        Name = r
                    });
                    role.Wait();

                    var userR= userManager.AddToRoleAsync(user, administrationRole);
                    //userR.Wait();
                }

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
}
