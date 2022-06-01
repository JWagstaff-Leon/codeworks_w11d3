using System;
using System.Collections.Generic;
using w11d3.Repositories;
using w11d3.Models;

namespace w11d3.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal List<Profile> GetAll()
        {
            return _repo.GetAll();
        }

        internal Profile GetById(string id)
        {
            Profile found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find profile with that id.");
            }
            return found;
        }
    }
}