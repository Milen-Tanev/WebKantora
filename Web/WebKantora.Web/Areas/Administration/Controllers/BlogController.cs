using AspNetSeo.CoreMvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using WebKantora.Web.Infrastructure;
using WebKantora.Web.Areas.Administration.Models.BlogViewModels;
using WebKantora.Services.Data.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    [SeoMetaRobotsNoIndex()]
    public class BlogController : Controller
    {
        private IUserService usersService;
        private IArticleService articlesService;
        private IKeywordService keywordsService;
        private IMemoryCache cache;
        private IMapper mapper;

        public BlogController(
            IUserService usersService,
            IArticleService articlesService,
            IKeywordService keywordsService,
            IMemoryCache cache,
            IMapper mapper)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.keywordsService = keywordsService;
            this.cache = cache;
            this.mapper = mapper;
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        // GET: Blog/Create
        public ActionResult CreateArticle()
        {
            var keywords = this.keywordsService.GetAll().ToList();

            var model = new CreateArticleViewModel()
            {
                AllKeywords = keywords.Select(x => x.Content).ToList()
            };

            return View(model);
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel model)
        {
            try
            {
                var text = await FileHelpers.ProcessFormFile(model.ArticleContent, ModelState);
                if (ModelState.IsValid)
                {
                    //TODO: try catch ?
                    var article = this.mapper.Map<Article>(model);
                    var user = await this.usersService.GetByUserName(User.Identity.Name);
                    article.Content = text;
                    article.Author = user;
                    article.Date = DateTime.UtcNow;
                    article.IsDeleted = false;

                    var keywords = model.Keywords;
                    var dbKeywords = this.keywordsService.GetAll();
                    var articleKeywordsList = new HashSet<Keyword>();
                    var keywordsArticles = new HashSet<KeywordArticle>();

                    foreach (var keyword in keywords)
                    {
                        var newKeyword = dbKeywords.Where(x => x.Content.ToLower() == keyword.ToLower()).FirstOrDefault();

                        if (newKeyword == null)
                        {
                            newKeyword = new Keyword()
                            {
                                Content = keyword
                            };
                            await this.keywordsService.Add(newKeyword);
                        }

                        //articleKeywordsList.Add(newKeyword);

                        var newKeywordArticle = new KeywordArticle()
                        {
                            KeywordId = newKeyword.Id,
   //                         Keyword = newKeyword,
                            ArticleId = article.Id,
 //                           Article = article
                        };

                        keywordsArticles.Add(newKeywordArticle);
                        // newKeyword.KeywordArticles.Add(newKeywordArticle);
                        // await this.keywordsService.Update(newKeyword.Id, newKeyword);
                        article.KeywordArticles.Add(newKeywordArticle);
                    }

                    this.cache.Remove("ArticlesCache");

                    await this.articlesService.Add(article);

                    /// Adds collection of keywordsArticles
                    //await this.keywordArticlesService.Add(keywordsArticles);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [HttpPost]
        public PartialViewResult AddKeyword(CreateArticleViewModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    var keyword = model.Keyword.ToLower().Trim();
                    if (!string.IsNullOrEmpty(keyword) && !model.Keywords.Contains(keyword))
                    {
                        model.Keywords.Add(keyword);
                    }
                    else
                    {
                        ModelState.AddModelError("Keyword", "Ключовата дума вече е добавена към тази статия");
                    }
                }
                else
                {
                    ModelState.AddModelError("Keyword", "Невалидна стойност за ключова дума");
                }
            }
            catch(Exception ex)
            {
                //todo: save error in db
            }
            model.Keyword = null;
            return PartialView("_AllKeywords", model);
        }
        /*
        // GET: Blog/Delete/5
        public ActionResult DeleteArticle(int id)
        {
            return View();
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteArticle(Guid id)
        {
            try
            {
                await this.articlesService.Delete(id);

                return Redirect("/Blog");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        
        //TODO: edit articles

        /*
        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}