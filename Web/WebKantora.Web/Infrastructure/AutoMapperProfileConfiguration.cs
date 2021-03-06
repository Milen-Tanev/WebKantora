﻿using AutoMapper;
using WebKantora.Data.Models;
using WebKantora.Web.Areas.Administration.Models.BlogViewModels;
using WebKantora.Web.Models.ArticleViewModels;
using WebKantora.Web.Models.ContactViewModels;

namespace WebKantora.Web.Infrastructure
{
    public class AutoMapperProfileConfiguration: Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
            :base(profileName)
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => $"{x.Content.Substring(0, 200)}..."));
            CreateMap<Article, FullArticleViewModel>()
                .ForMember(x => x.Author, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"));
            CreateMap<ContactFormViewModel, Message>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<CreateArticleViewModel, Article>()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title));
        }
    }
}
