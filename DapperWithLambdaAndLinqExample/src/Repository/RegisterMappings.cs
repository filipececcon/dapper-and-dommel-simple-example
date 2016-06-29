using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Repository.Mappers;

namespace Repository
{
    public static class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new PlayerMap());
                config.AddMap(new TeamMap());
                config.ForDommel();
            });
        }
    }
}