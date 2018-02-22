using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

using WebKantora.Data.Common.Contracts;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var assembly = typeof(IUsersService).GetTypeInfo().Assembly;
            assembly.GetTypes()
                .Where(t => t.GetType().GetTypeInfo().IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetTypeInfo().GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            var assembly = typeof(IArticleDbRepository).GetTypeInfo().Assembly;
            assembly.GetTypes()
                .Where(t => t.GetType().GetTypeInfo().IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetTypeInfo().GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}
