using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using PagedList.Core;

using WebKantora.Web.Infrastructure.Mappings;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Models.ArticleViewModels;
using System.Linq;
using System.Collections.Generic;

namespace WebKantora.Web.Controllers
{
    public class BlogController: Controller
    {
        private IArticlesService articlesService;
        private IMapper mapper;
        private IMemoryCache cache;

        public BlogController(IArticlesService articlesService, IMapper mapper, IMemoryCache cache)
        {
            this.articlesService = articlesService;
            this.mapper = mapper;
            this.cache = cache;
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            // Keyword[] parameter
            if (!cache.TryGetValue("ArticlesCache", out ICollection<ArticleViewModel> cacheEntry))
            {
                cacheEntry = this.articlesService.GetAll()
                .To<ArticleViewModel>().ToList();

                cache.Set("ArticlesCache", cacheEntry);
            }
            var viewModel = cacheEntry.AsQueryable().ToPagedList(page, 5);
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ById(Guid id)
        {
            var article = await this.articlesService.GetById(id);
            var viewModel = this.mapper.Map<FullArticleViewModel>(article);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult ByKeyword(Guid keywordId, int page = 1)
        {
            var viewModel = this.articlesService.GetByKeyword(keywordId)
                .To<ArticleViewModel>().ToPagedList(page, 2);

            ViewBag.KeywordId = keywordId;

            return this.View(viewModel);
        }
    }
}
