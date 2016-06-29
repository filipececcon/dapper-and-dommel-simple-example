using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Domain.Entities;

namespace Repository.OnlyDapper
{
    public class TeamRepository : RepositoryBase<Team>
    {
        public override IEnumerable<Team> GetAll()
        {
            const string sql = @"SELECT * FROM TB_TIME";

            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query(sql).Select(x => new Team
                {
                    Id = x.ID,
                    Name = x.NM_TIME
                });
            }
        }

        public override Team GetById(int id)
        {
            const string sql = "SELECT * FROM TB_TIME WHERE Id = @id";

            using (var db = new SqlConnection(ConnectionString))
            {
                var team = db.Query(sql, new { id }).SingleOrDefault();

                return new Team
                {
                    Id = team.ID,
                    Name = team.NM_TIME
                };
            }
        }

        public override void Insert(ref Team entity)
        {
            const string sql =
                @"INSERT INTO TB_TIME (NM_TIME) VALUES(@nome);
                  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = new SqlConnection(ConnectionString))
            {
                var id = db.Query<int>(sql).Single();

                entity = GetById(id);
            }

        }

        public override bool Update(Team entity)
        {
            const string sql = @"UPDATE TB_TIME SET NM_TIME = @Name WHERE ID = @Id";

            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Execute(sql, entity) == 1;
            }
        }

        public override bool Delete(Int32 id)
        {
            const string sql = @"DELETE FROM TB_TIME WHERE ID = @id";

            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Execute(sql, new { id }) == 1;
            }
        }

        public override IEnumerable<Team> GetList(Expression<Func<Team, bool>> predicate)
        {
            throw new NotImplementedException();
            //This approach would be extremely complex to implement, it is not the focus now.
            //Esta abordagem seria extremamente complexa de implementar, não é o foco agora.
        }
    }
}