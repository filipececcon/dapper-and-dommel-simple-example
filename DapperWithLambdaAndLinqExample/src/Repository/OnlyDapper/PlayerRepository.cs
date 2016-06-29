using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Domain.Entities;

namespace Repository.OnlyDapper
{
    public class PlayerRepository : RepositoryBase<Player>
    {
        public override IEnumerable<Player> GetAll()
        {
            const string sql = "SELECT J.*, T.NM_TIME FROM TB_JOGADOR AS J INNER JOIN TB_TIME AS T ON J.ID_TIME = T.ID";

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.Query(sql);

                return result.Select(player => new Player
                {
                    Id = player.ID,
                    Name = player.NM_JOGADOR,
                    Age = player.NR_IDADE,
                    Team = new Team
                    {
                        Id = player.ID_TIME,
                        Name = player.NM_TIME
                    }
                });

            }
        }
        
        public override Player GetById(int id)
        {
            const string sql =
                @"SELECT * FROM TB_JOGADOR AS J 
                    INNER JOIN TB_TIME AS T
                    ON J.ID_TIME = T.ID
                  WHERE J.ID = @idPlayer";

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.Query(sql, new {idPlayer = id}).SingleOrDefault();

                return new Player
                {
                    Id = result.ID,
                    Name = result.NM_JOGADOR,
                    Age = result.NR_IDADE,
                    Team = new Team
                    {
                        Id = result.ID_TIME,
                        Name = result.NM_TIME
                    }
                };

            }
        }

        public override void Insert(ref Player entity)
        {
            const string sql =
                @"INSERT INTO TB_JOGADOR (NM_JOGADOR, NR_IDADE, ID_TIME) VALUES (@Nome, @Idade, @timeId);
                 SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new
                {
                    Nome = entity.Name,
                    Idade = entity.Age,
                    timeId = entity.Team.Id
                };
            
                // retornar o id inserido
                var id = db.Query<int>(sql, parameters).Single();

                entity = GetById(id);
            }
        }

        public override bool Update(Player entity)
        {
            const string sql = 
                @"UPDATE TB_JOGADOR SET 
                    NM_JOGADOR = @nome,
                    NR_IDADE = @idade,
                    ID_TIME = @timeId
                WHERE ID = @Id";

            var parameters = new
            {
                nome = entity.Name,
                idade = entity.Age,
                timeId = entity.Team.Id,
                id = entity.Id
            };

            using (var db = new SqlConnection(ConnectionString))
            {
                //se a quantidade de linhas afetadas foi diferente de 0
                return db.Execute(sql, parameters) != 0;
            }
        }

        public override bool Delete(Int32 id)
        {
            const string sql = @"DELETE FROM TB_JOGADOR WHERE ID = @id";

            using (var db = new SqlConnection(ConnectionString))
            {
                //se a quantidade de linhas afetadas foi diferente de 0
                return db.Execute(sql, new {id}) != 0;
            }
        }

        public override IEnumerable<Player> GetList(Expression<Func<Player, bool>> predicate)
        {
            throw new NotImplementedException();
            //This approach would be extremely complex to implement, it is not the focus now.
            //Esta abordagem seria extremamente complexa de implementar, não é o foco agora.
        }
    }
}