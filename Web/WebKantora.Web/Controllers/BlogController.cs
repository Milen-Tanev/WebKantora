using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using PagedList.Core;

using WebKantora.Web.Infrastructure.Mappings;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Models.ArticleViewModels;
using System.Threading.Tasks;

namespace WebKantora.Web.Controllers
{
    public class BlogController: Controller
    {
        private IArticlesService articlesService;
        private IMapper mapper;

        public BlogController(IArticlesService articlesService, IMapper mapper)
        {
            this.articlesService = articlesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            // Keyword[] parameter
            var viewModel = this.articlesService.GetAll().To<ArticleViewModel>().ToPagedList(page, 3);
            
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ById(Guid id)
        {
            var article = await this.articlesService.GetById(id);
            var viewModel = this.mapper.Map<ArticleViewModel>(article);

            return this.View(viewModel);
        }
    }
}
