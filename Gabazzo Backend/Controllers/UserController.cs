using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.CommonModels;
using Gabazzo_Backend.Models.InputModels.UserModels;
using Gabazzo_Backend.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gabazzo_Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IConfiguration configuration;
        public UserController(IUserServices _userServices,IConfiguration _configuration)
        {
            userServices = _userServices;
            configuration = _configuration;
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMonths(1),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserRegistration ToLower(UserRegistration userRegistration)
        {
            try
            {
                userRegistration.UserId = Guid.NewGuid();
                userRegistration.UserFirstName = userRegistration.UserFirstName.ToString().ToLower();
                userRegistration.UserLastName = userRegistration.UserLastName.ToString().ToLower();
                userRegistration.UserName = userRegistration.UserName.ToLower();
                userRegistration.Email = userRegistration.Email.ToLower();
                userRegistration.Role = userRegistration.Role.ToString().ToLower();
                userRegistration.Password = userRegistration.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password);
                return userRegistration;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [AllowAnonymous]
        [HttpPost("UserRegistration")]
        public async Task<IActionResult> CreateUser([FromBody]UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userRegistration = ToLower(userRegistration);
                    var tempuser = await userServices.CreateUser(userRegistration);
                    if (tempuser != null)
                    {
                        var tokenString = GenerateJSONWebToken();
                        var response = Ok(new { tokenString });
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }


            }
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("UserLogin")]
        public async Task<IActionResult> Login([FromBody] Login userCradentials)
        {
            IActionResult response = Unauthorized();

            RegisteredUser user = await userServices.GetUser(userCradentials.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(userCradentials.Password, user.Password) == true)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { tokenString });
            }
            return response;
        }



    }
}