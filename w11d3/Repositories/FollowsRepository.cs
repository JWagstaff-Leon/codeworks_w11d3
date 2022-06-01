using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using w11d3.Models;

namespace w11d3.Repositories
{
    public class FollowsRepository
    {
        private readonly IDbConnection _db;

        public FollowsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Follow GetById(int id)
        {
            string sql = @"
            SELECT
            *
            FROM follows
            WHERE id = @id;
            ";
            return _db.Query<Follow>(sql, new { id }).FirstOrDefault();
        }

        internal Follow GetByBoth(string follower, string following)
        {
            string sql = @"
            SELECT
            *
            FROM follows
            WHERE follower = @follower AND following = @following;
            ";
            return _db.Query<Follow>(sql, new { follower, following }).FirstOrDefault();
        }
        
        internal List<FollowVM> GetByFollower(string follower)
        {
            string sql = @"
            SELECT
            acc.*,
            fol.id AS followId
            FROM follows fol
            JOIN accounts acc ON fol.following = acc.id
            WHERE fol.follower = @follower;
            ";
            return _db.Query<FollowVM>(sql, new { follower }).ToList();
        }

        internal List<FollowVM> GetByFollowing(string following)
        {
            string sql = @"
            SELECT
            acc.*,
            fol.id AS followId
            FROM follows fol
            JOIN accounts acc ON fol.follower = acc.id
            WHERE fol.following = @following;
            ";
            return _db.Query<FollowVM>(sql, new { following }).ToList();
        }

        internal Follow Create(Follow data)
        {
            string sql = @"
            INSERT
            INTO follows
            (follower, following)
            VALUES
            (@Follower, @Following);
            SELECT LAST_INSERT_ID();
            ";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            return data;
        }

        internal void Remove(int id)
        {
            string sql = @"
            DELETE
            FROM follows
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}