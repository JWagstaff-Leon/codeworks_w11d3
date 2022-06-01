using System;
using System.Collections.Generic;
using w11d3.Models;
using w11d3.Repositories;

namespace w11d3.Services
{
    public class FollowsService
    {
        private readonly FollowsRepository _repo;

        public FollowsService(FollowsRepository repo)
        {
            _repo = repo;
        }

        internal Follow GetById(int id)
        {
            Follow found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find follow with that id.");
            }
            return found;
        }

        internal List<FollowVM> GetByFollower(string follower)
        {
            return _repo.GetByFollower(follower);
        }

        internal List<FollowVM> GetByFollowing(string following)
        {
            return _repo.GetByFollowing(following);
        }

        internal Follow Create(Follow data)
        {
            Follow found = _repo.GetByBoth(data.Follower, data.Following);
            if(found != null)
            {
                return found;
            }
            return _repo.Create(data);
        }

        internal Follow Remove(int id, string userId)
        {
            Follow removed = GetById(id);
            if(removed.Follower != userId)
            {
                throw new Exception("You do not have permission to delete this follow.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}