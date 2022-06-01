using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using w11d3.Models;
using w11d3.Services;

namespace w11d3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _serv;
        private readonly FollowsService _followServ;

        public ProfilesController(ProfilesService serv, FollowsService followServ)
        {
            _serv = serv;
            _followServ = followServ;
        }

        [HttpGet]
        public ActionResult<List<Profile>> GetAll()
        {
            try
            {
                List<Profile> found = _serv.GetAll();
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetById(string id)
        {
            try
            {
                Profile found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/followers")]
        public ActionResult<List<FollowVM>> GetByFollowing(string id)
        {
            try
            {
                List<FollowVM> found = _followServ.GetByFollowing(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/following")]
        public ActionResult<List<FollowVM>> GetByFollower(string id)
        {
            try
            {
                List<FollowVM> found = _followServ.GetByFollower(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}