using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabazzo_Backend.Models.DbModels;
using Gabazzo_Backend.Models.InputModels.CommonModels;
using Gabazzo_Backend.Models.InputModels.ContractorsModels;
using Gabazzo_Backend.Repository.ContractorRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gabazzo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly IContractorServices contractorServices ;
        private readonly IConfiguration configuration;
        public ContractorController(IContractorServices _contractorServices, IConfiguration _configuration)
        {
            contractorServices = _contractorServices;
            configuration = _configuration;
        }

        private ContractorRegistration ToLower(ContractorRegistration contractorRegistration)
        {
            try
            {
                contractorRegistration.ContractorId = Guid.NewGuid();
                contractorRegistration.ContractorFirstName = contractorRegistration.ContractorFirstName.ToLower();
                contractorRegistration.ContractorLastName = contractorRegistration.ContractorLastName.ToLower();
                contractorRegistration.UserName = contractorRegistration.UserName.ToLower();
                contractorRegistration.Email = contractorRegistration.Email.ToLower();
                contractorRegistration.CompanyAddress = contractorRegistration.CompanyAddress.ToLower();
                contractorRegistration.CompanyName = contractorRegistration.CompanyName;
                contractorRegistration.Description = contractorRegistration.Description;
                contractorRegistration.Logo = contractorRegistration.Logo;
                contractorRegistration.Password = BCrypt.Net.BCrypt.HashPassword(contractorRegistration.Password);
                contractorRegistration.Role = contractorRegistration.Role.ToLower();
                return contractorRegistration;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
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

        [AllowAnonymous]
        [HttpPost("ContractorRegistration")]
        public async Task<IActionResult> CreateUser([FromBody]ContractorRegistration contractorRegistration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contractorRegistration = ToLower(contractorRegistration);
                    var tempuser = await contractorServices.CreateContractor(contractorRegistration);
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
        [HttpPost("ContractorLogin")]
        public async Task<IActionResult> Login([FromBody] Login userCradentials)
        {
            IActionResult response = Unauthorized();

            RegisteredContractor user = await contractorServices.GetContractor(userCradentials.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(userCradentials.Password, user.Password) == true)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { tokenString });
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("CreatePortfolio")]
        public async Task<IActionResult> CreatePortfolio([FromBody]Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempPortfolio = await contractorServices.CreatePortolio(portfolio);
                    if (tempPortfolio != null)
                    {
                        return Ok(tempPortfolio);
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
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService([FromBody]Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempService = await contractorServices.CreateService(service);
                    if (tempService != null)
                    {
                        return Ok(tempService);
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
        [HttpGet("GetPortfolioById")]
        public async Task<IActionResult> GetPortfolioById(string Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempPortfolio = await contractorServices.GetPortfolioById(Id);
                    if (tempPortfolio != null)
                    {
                        return Ok(tempPortfolio);
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
        [HttpGet("GetServiceByServiceId")]
        public async Task<IActionResult> GetServiceByServiceId(string Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempservice = await contractorServices.GetServiceByServiceId(Id);
                    if (tempservice != null)
                    {
                        return Ok(tempservice);
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
        [HttpGet("GetServiceByContractorId")]
        public async Task<IActionResult> GetServiceByContractorId(string Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempservices = await contractorServices.GetServiceByContractorId(Id);
                    if (tempservices != null)
                    {
                        return Ok(tempservices);
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



    }
}