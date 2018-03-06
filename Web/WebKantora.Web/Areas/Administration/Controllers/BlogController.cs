using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebKantora.Web.Infrastructure;
using WebKantora.Web.Areas.Administration.Models.BlogViewModels;
using AutoMapper;
using WebKantora.Services.Data.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private IUsersService usersService;
        private IArticlesService articlesService;
        private IKeywordsService keywordsService;
        private IMapper mapper;

        public BlogController(IUsersService usersService, IArticlesService articlesService, IKeywordsService keywordsService, IMapper mapper)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.keywordsService = keywordsService;
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
                if(ModelState.IsValid)
                {
                    var article = this.mapper.Map<Article>(model);
                    var user = await this.usersService.GetByUserName(User.Identity.Name);
                    article.Content = text;
                    article.Author = user;
                    article.Date = DateTime.UtcNow;

                    //TODO: add keywords

                    await this.articlesService.Add(article);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
         
        public async Task<PartialViewResult> AddKeyword(CreateArticleViewModel model)
        {
            /*
            var newKeyWord = new Keyword()
            {
                Content = model.Keyword
            };
            */
            model.Keywords.Add(model.Keyword);
            return PartialView("_AllKeywords", model);
        }

        //TODO: edit and delete articles

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

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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