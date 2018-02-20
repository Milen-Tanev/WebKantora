using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebKantora.Web.Infrastructure.Mappings;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Models.ArticleViewModels;

namespace WebKantora.Web.Controllers
{
    public class BlogController: Controller
    {
        private IArticlesService articlesService;

        public BlogController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpGet]
        public ActionResult Articles()
        {
            var allArticles = this.articlesService.GetAll();

            var viewModel = this.articlesService.GetAll().To<ArticleViewModel>().ToList();
            
            return this.View(viewModel);
        }
    }
}
