using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebKantora.Data.Models;
using WebKantora.Web.Infrastructure.Mappings.Contracts;

namespace WebKantora.Web.Models.ArticleViewModels
{
    public class ArticleViewModel: IMapFrom<Article>, ICustomMap
    {
        public Guid Id { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<KeywordArticle> KeywordArticles { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"));
        }
    }
}
