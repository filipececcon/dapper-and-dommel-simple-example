using Dapper.FluentMap.Dommel.Mapping;
using Domain.Entities;

namespace Repository.Mappers
{
    public class PlayerMap : DommelEntityMap<Player>
    {
        // * PRESTE ATENÇÃO NOS NOMES INFORMADOS DE TABELAS E COLUNAS, POIS SÃO 'caseSentitive'
        public PlayerMap()
        {
            ToTable("TB_JOGADOR");

            //para a chave primária da tabela use 'IdKey()'
            Map(x => x.Id).ToColumn("ID").IsKey();
            
            Map(x => x.Age).ToColumn("NR_IDADE");
            Map(x => x.Name).ToColumn("NM_JOGADOR");

            //mapeie as colunas FK, é prioridade para os JOINS
            Map(x => x.TeamId).ToColumn("ID_TIME");

            //ignore as subclasses no mapeamento e considere apenas a FK
            Map(x => x.Team).Ignore(); 
        }
    }
}