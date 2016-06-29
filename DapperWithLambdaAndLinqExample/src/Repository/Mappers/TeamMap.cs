using Dapper.FluentMap.Dommel.Mapping;
using Domain.Entities;

namespace Repository.Mappers
{
    public class TeamMap : DommelEntityMap<Team>
    {
        public TeamMap()
        {
            ToTable("TB_TIME");
            Map(x => x.Id).ToColumn("ID").IsKey();
            Map(x => x.Name).ToColumn("NM_TIME");
        }
    }
}