using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w11d3.Services;
using w11d3.Models;
using CodeWorks.Auth0Provider;

namespace w11d3.Controllers
{
    [ApiController]
    [Route("api/follow")]
    [Authorize]
    public class FollowsController : ControllerBase
    {
        private readonly FollowsService _serv;

        public FollowsController(FollowsService serv)
        {
            _serv = serv;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Follow>> Create(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Follow data = new Follow() { Follower = userInfo.Id, Following = id };
                Follow created = _serv.Create(data);
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Follow>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Follow removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}