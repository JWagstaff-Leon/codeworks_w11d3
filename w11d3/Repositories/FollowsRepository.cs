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

        internal FollowVM GetByBoth(string follower, string following)
        {
            string sql = @"
            SELECT
            id
            FROM follows
            WHERE follower = @follower AND following = @following;
            ";

            int id = _db.ExecuteScalar<int>(sql, new { follower, following });

            sql = @"
            SELECT 
                acc.*,
                fol.id AS followId
            FROM follows fol
            JOIN accounts acc ON fol.following = acc.id
            WHERE fol.id = @id;
            ";

            return _db.Query<FollowVM>(sql, new { id }).FirstOrDefault();
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

        internal FollowVM Create(Follow data)
        {
            string sql = @"
            INSERT
            INTO follows
            (follower, following)
            VALUES
            (@Follower, @Following);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);

            sql = @"
            SELECT 
                acc.*,
                fol.id AS followId
            FROM follows fol
            JOIN accounts acc ON fol.following = acc.id
            WHERE fol.id = @id;
            ";
            return _db.Query<FollowVM>(sql, new { id }).FirstOrDefault();
        }

        internal FollowVM Remove(int id)
        {
            string sql = @"
            SELECT 
                acc.*,
                fol.id AS followId
            FROM follows fol
            JOIN accounts acc ON fol.following = acc.id
            WHERE fol.id = @id;
            ";

            FollowVM removed = _db.Query<FollowVM>(sql, new { id }).FirstOrDefault();

            sql = @"
            DELETE
            FROM follows
            WHERE id = @id
            LIMIT 1;
            ";
            _db.Execute(sql, new { id });

            return removed;
        }
    }
}