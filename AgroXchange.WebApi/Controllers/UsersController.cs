using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AgroXchange.WebApi.Services;
using AgroXchange.WebApi.Models;
using AgroXchange.WebApi.Utils;
using AgroXchange.Data;
using AgroXchange.Data.Models;

namespace AgroXchange.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserView userParam)
        {
            try
            {
                EFDataContext _dbContext = new EFDataContext();
                UserView dbUser = _dbContext.Users
                    .Where(u => u.EmailId == userParam.EmailId.ToLower() 
                        && !u.LockedOut && u.Activated)
                    .Select(u => new UserView {
                        Id = u.UserId,
                        EmailId = u.EmailId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Role = u.UserRole.RoleName,
                        Password = u.PasswordHash
                    })
                    .FirstOrDefault();
                if (dbUser == null)
                    throw new ApiException("Either username or password is incorrect or user is not activated or locked out");
                string hash = dbUser.Password;
                if (!_userService.VerifyPasswordHash(dbUser, hash, userParam.Password))
                    throw new ApiException("Username or password is incorrect");

                var token = _userService.GenerateToken(dbUser.Id);

                if (token == null)
                    throw new ApiException("Username or password is incorrect");

                var resp = new UserAuthResponse();
                UserView userResp = new UserView {
                    Id = dbUser.Id,
                    EmailId = dbUser.EmailId,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Role = dbUser.Role
                };
                resp.User = userResp;
                resp.Token = token;

                return Ok(resp);
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                    return BadRequest(ex);
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PostUser([FromBody]UserView userParam)
        {
            try
            {
                EFDataContext _dbContext = new EFDataContext();
                User newUser = new User();
                newUser.UserId = userParam.Id = Guid.NewGuid();
                newUser.FirstName = userParam.FirstName;
                newUser.LastName = userParam.LastName;
                newUser.EmailId = userParam.EmailId.ToLower();
                newUser.PasswordHash = _userService.GeneratePasswordHash(userParam, userParam.Password);
                newUser.PasswordSalt = "-";
                newUser.DateCreated = DateTime.UtcNow;
                newUser.Activated = true; //TODO: need to change to false when email and key are implemented
                newUser.ActivationKey = "";
                newUser.LockedOut = false;
                newUser.RoleId = _dbContext.Roles.Where(r => r.RoleName == userParam.Role).Select(r => r.RoleId).FirstOrDefault();
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                    return BadRequest(ex);
                return BadRequest(new { message = "Error saving new user. Please try again." });
            }
        }
    }
}