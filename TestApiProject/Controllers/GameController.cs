using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using TestApiProject.Data;
using TestApiProject.Dtos;
using TestApiProject.Entities;
using TestApiProject.Services;

namespace TestApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CharacterService _characterService;
        private readonly SuperPowerService _superPowerService;

        public GameController(UserService userService, CharacterService characterService, SuperPowerService superPowerService)
        {
            _userService = userService;
            _characterService = characterService;
            _superPowerService = superPowerService;
        }

        #region UserServices
        
        [HttpGet("Users/{userId}", Name = "GetUserById") ]
        public ActionResult<UserGetDto> GetUserById(int userId)
        {
            var result = _userService.GetUserById(userId);

            if (result == null)
            {
                NotFound();
            }

            return Ok(result);

        }

        [HttpGet("Users")]
        public ActionResult<UserGetDto> GetUsers()
        {
            var result = _userService.GetUsers();
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Users")]
        public ActionResult<UserGetDto> PostUsers(UserPostDto input)
        {
            var result = _userService.CreateUser(input);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("Users")]
        public ActionResult<bool> DeleteUser(int userId)
        {
            var result = _userService.DeleteUser(userId);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }

        [HttpPut("Users")]
        public ActionResult<bool> PutUser(UserPutDto input)
        {
            var result = _userService.UpdateUser(input);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }
        #endregion

        #region SuperPowerServices

        [HttpGet("SuperPowers")]
        public ActionResult<SuperPowerGetDto> GetSuperPowers()
        {
            var result = _superPowerService.GetSuperPowers();
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }


        [HttpPost("SuperPowers")]
        public ActionResult<SuperPowerGetDto> PostSuperPowers(SuperPowerPostDto input)
        {
            var result = _superPowerService.CreateSuperPower(input);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }


        [HttpDelete("SuperPowers")]
        public ActionResult<bool> DeleteSuperPower(int superPowerId)
        {
            var result = _superPowerService.DeleteUser(superPowerId);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }


        [HttpPut("SuperPowers")]
        public ActionResult<bool> PutSuperPower(SuperPowerPutDto input)
        {
            var result = _superPowerService.UpdateSuperPower(input);
            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }
        #endregion

        #region CharacterServices

        #endregion
    }
}
