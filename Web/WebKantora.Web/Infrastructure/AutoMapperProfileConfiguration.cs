using AutoMapper;
using WebKantora.Data.Models;
using WebKantora.Web.Models.ArticleViewModels;

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
                .ForMember(x => x.Author, opt => opt.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"));
        }
    }
}
