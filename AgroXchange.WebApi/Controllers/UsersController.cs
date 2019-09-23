using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using AgroXchange.WebApi.Services;
using AgroXchange.WebApi.Models;
using AgroXchange.WebApi.Utils;
using AgroXchange.WebApi.Helpers;
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
        private IMailService _mailService;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMailService mailService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mailService = mailService;
            _appSettings = appSettings.Value;
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
                    return BadRequest(new { message = ex.Message });
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
                newUser.Activated = false;
                newUser.ActivationKey = CryptoUtils.GenerateRandomString(20);
                newUser.ActivationMailDate = DateTime.UtcNow;
                newUser.LockedOut = false;
                newUser.RoleId = _dbContext.Roles.Where(r => r.RoleName == userParam.Role).Select(r => r.RoleId).FirstOrDefault();
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                string activationLink = _appSettings.WebUrl + "auth/activate?email=" + newUser.EmailId + "&key=" + newUser.ActivationKey;
                Mail newMail = new Mail {
                    Subject = "AgroXchange Account Activation",
                    BodyHtml = string.Format("Dear {0},<p>Thank you for signing up to AgroXchange. Please verify your email address by clicking the link below or copy-pasting it in a browser window.</p><p><a href=\"{1}\">{1}</a></p><p>Regards</p><p>AgroXchange</p>", newUser.FirstName + " " + newUser.LastName, activationLink) };
                newMail.AddToRecipient(newUser.FirstName + " " + newUser.LastName, newUser.EmailId);
                _mailService.SendMail(newMail);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                    return BadRequest(new { message = ex.Message });
                else if (ex.InnerException != null && ex.InnerException.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.Users' with unique index 'IX_Users_EmailId'"))
                    return BadRequest(new { message = "A user already exists with the email id specified. Please login with this email id or register another one." });
                return BadRequest(new { message = "Error saving new user. Please try again." });
            }
        }

        [AllowAnonymous]
        [HttpPost("activate")]
        public IActionResult Activate([FromBody]UserActivation userParam)
        {
            try
            {
                EFDataContext _dbContext = new EFDataContext();
                User dbUser = _dbContext.Users
                    .Where(u => u.EmailId == userParam.Email.ToLower()
                        && u.ActivationKey == userParam.Key
                        && !u.Activated).FirstOrDefault();
                if (dbUser == null)
                    throw new ApiException("The email id given either does not exist or the key provided is invalid, or this user has already been activated");
                if (!dbUser.ActivationMailDate.HasValue || dbUser.ActivationMailDate.Value.AddHours(24).CompareTo(DateTime.UtcNow) < 0)
                {
                    dbUser.ActivationKey = CryptoUtils.GenerateRandomString(20);
                    dbUser.ActivationMailDate = DateTime.UtcNow;
                    _dbContext.SaveChanges();
                    string activationLink = _appSettings.WebUrl + "auth/activate?email=" + dbUser.EmailId + "&key=" + dbUser.ActivationKey;
                    Mail newMail = new Mail
                    {
                        Subject = "AgroXchange Account Activation",
                        BodyHtml = string.Format("Dear {0},<p>We have generated a new activation key for you. Please verify your email address by clicking the link below or copy-pasting it in a browser window.</p><p><a href=\"{1}\">{1}</a></p><p>Regards</p><p>AgroXchange</p>", dbUser.FirstName + " " + dbUser.LastName, activationLink)
                    };
                    newMail.AddToRecipient(dbUser.FirstName + " " + dbUser.LastName, dbUser.EmailId);
                    _mailService.SendMail(newMail);
                    throw new ApiException("It's been more than 24 hours. Your activation key has expired. We have emailed you a new one. Please follow the link in the new email to activate your account.");
                }
                else
                {
                    dbUser.Activated = true;
                    dbUser.ActivationKey = "";
                    _dbContext.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                    return BadRequest(new { message = ex.Message });
                return BadRequest(new { message = "Error while activating user. Please try again." });
            }
        }
    }
}
