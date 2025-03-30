using Microsoft.AspNetCore.Mvc;
using TPC_API.Models;
using TPC_API.Services;

namespace TPC_API.Controllers
{
    public class UserController :ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
           _userService = userService;
        }
        
        [HttpGet]
        [Route("[controller]")]
        public ActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public ActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Failure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpPost]
        [Route("[controller]")]
        public ActionResult Post(User user)
        {
            var result =_userService.Create(user);
            if (result.Failure) return BadRequest(result.Error);
            user = result.Value;
            return Created($"{Request.Path}/{user.Id}", user);
        }

        [HttpPut]
        [Route("[controller]/{id}")]
        public ActionResult Put(User user, int id)
        {
            if (user == null) return BadRequest();
            user.Id = id;
            var result = _userService.Update(user);
            if (result.Failure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _userService.RemuveById(id);
            if (result.Failure) return BadRequest(result.Error);
            return Ok();
        }
    }
}
