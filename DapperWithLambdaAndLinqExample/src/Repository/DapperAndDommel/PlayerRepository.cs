using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.Entities;
using Dommel;

namespace Repository.DapperAndDommel
{
    public class PlayerRepository : RepositoryBase<Player>
    {
        /*
        
            Sobrescrevemos a implementação padrão pois queremos implementar com JOIN.    
            
            Na instrução do Dommel GetAll passamos <classePai, subClasse, classeDeSaida> para fazer o join 

            Injetamos um retorno no outro no bloco de delegate, como tem que ser Time dentro de Jogador

            E retornamos o jogador populado para assim retornar a lista com join

            PS: não é executado o JOIN na aplicação e sim no banco com string sql

        */

        public override IEnumerable<Player> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.GetAll<Player, Team, Player>((player, team) =>
                {
                    player.Team = team;
                    return player;
                });
            }
        }


        //  Fazemos a mesma coisa que no método GetAll(), porém para apenas um registro passado o 'Id'    
        public override Player GetById(int idPlayer)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Get<Player, Team, Player>(idPlayer, (player, team) =>
                {
                    player.Team = team;
                    return player;
                });
            }
        }
        
    }
}