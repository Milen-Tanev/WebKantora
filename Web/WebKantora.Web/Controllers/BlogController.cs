using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using PagedList.Core;

using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Infrastructure.Mappings;
using WebKantora.Web.Models.ArticleViewModels;
using System.Linq;
using System.Collections.Generic;
using AspNetSeo.CoreMvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class BlogController: BaseController
    {
        private IArticleService articlesService;
        private IMapper mapper;
        private IMemoryCache cache;
        private ICustomErrorService customErrors;

        public BlogController(IArticleService articlesService, IMapper mapper, IMemoryCache cache, ICustomErrorService customErrors)
        {
            this.articlesService = articlesService;
            this.mapper = mapper;
            this.cache = cache;
            this.customErrors = customErrors;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            try
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
            catch(Exception ex)
            {
                var error = new CustomError()
                {
                    InnerException = "",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    CustomMessage = ""
                };

                await this.customErrors.Add(error);
                return this.View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> ById(Guid id)
        {
            try
            {
                var article = await this.articlesService.GetById(id);
                var viewModel = this.mapper.Map<FullArticleViewModel>(article);
                var keywordArticles = article.KeywordArticles;

                foreach (var ka in keywordArticles)
                {
                    this.GetSeoHelper().AddMetaKeyword(ka.Keyword.Content);
                }

                return this.View(viewModel);
            }
            catch(Exception ex)
            {
                var error = new CustomError()
                {
                    InnerException = "",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    CustomMessage = ""
                };

                await this.customErrors.Add(error);
                return this.View();
            }

        }

        [HttpGet]
        public async Task<ActionResult> ByKeyword(Guid keywordId, int page = 1)
        {
            try
            {
                var viewModel = this.articlesService.GetByKeyword(keywordId)
                    .To<ArticleViewModel>().ToPagedList(page, 2);

                ViewBag.KeywordId = keywordId;

                return this.View(viewModel);
            }
            catch(Exception ex)
            {
                var error = new CustomError()
                {
                    InnerException = "",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    CustomMessage = ""
                };

                await this.customErrors.Add(error);

                return this.View();
            }
        }
    }
}
