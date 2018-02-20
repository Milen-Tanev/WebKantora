using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebKantora.Web.Infrastructure.Mappings.Contracts;

namespace WebKantora.Web.Infrastructure.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapperConfigurationExpression Configuration { get; private set; }

        public void Execute(Assembly assembly)
        {
            Mapper.Initialize(cfg =>
            {
                var types = assembly.GetExportedTypes();
                LoadStandardMapping(types, cfg);
                LoadReverseMappings(types, cfg);
                LoadCustomMappings(types, cfg);
            });
        }
        private static void LoadStandardMapping(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType && i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType &&
                              i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMap).IsAssignableFrom(t) &&
                              !t.GetTypeInfo().IsAbstract &&
                              !t.GetTypeInfo().IsInterface
                        select (ICustomMap)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}
