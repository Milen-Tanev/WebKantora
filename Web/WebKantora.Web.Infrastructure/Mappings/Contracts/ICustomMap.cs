using AutoMapper;

namespace WebKantora.Web.Infrastructure.Mappings.Contracts
{
    public interface ICustomMap
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
