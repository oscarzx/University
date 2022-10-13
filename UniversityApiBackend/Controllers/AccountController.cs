using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        //Example Users
        //TODO: Change by real users in DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Mail = "oscar@yopmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Mail = "pepe@yopmail.com",
                Name = "User1",
                Password = "pepe"
            },
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                //var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                var Valid = _context.Users.Any(user => user.Name == userLogin.UserName  && user.Password==userLogin.Password);

                
                if (Valid)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    var user = _context.Users.FirstOrDefault(user => user.Name == userLogin.UserName && user.Password == userLogin.Password);

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Mail,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
