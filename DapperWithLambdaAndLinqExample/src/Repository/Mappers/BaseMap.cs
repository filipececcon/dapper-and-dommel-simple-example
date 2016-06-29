using Dapper.FluentMap.Dommel.Mapping;
using Domain.Entities;

namespace Repository.Mappers
{
    public class BaseMap : DommelEntityMap<BaseEntity>
    {
        public BaseMap()
        {
            Map(x => x.Id).Ignore();
        }
    }
}