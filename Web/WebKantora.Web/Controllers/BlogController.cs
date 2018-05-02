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
        private IArticleService articles;
        private IMapper mapper;
        private IMemoryCache cache;
        private ICustomErrorService customErrors;

        public BlogController(IArticleService articles, IMapper mapper, IMemoryCache cache, ICustomErrorService customErrors)
        {
            this.articles = articles ?? throw new ArgumentNullException("Articles service cannot be null.");
            this.mapper = mapper ?? throw new ArgumentNullException("Mapper cannot be null.");
            this.cache = cache ?? throw new ArgumentNullException("Cache cannot be null.");
            this.customErrors = customErrors ?? throw new ArgumentNullException("CustomError service cannot be null.");
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                // Keyword[] parameter
                if (!cache.TryGetValue("ArticlesCache", out ICollection<ArticleViewModel> cacheEntry))
                {
                    cacheEntry = this.articles.GetAll()
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
                var article = await this.articles.GetById(id);
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
                var viewModel = this.articles.GetByKeyword(keywordId)
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
