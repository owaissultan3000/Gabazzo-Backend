using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gabazzo_Backend.Repository.CommonRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gabazzo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService commonService;
        private readonly IConfiguration configuration;
        public CommonController(ICommonService _commonService, IConfiguration _configuration)
        {
            commonService = _commonService;
            configuration = _configuration;
        }

        [AllowAnonymous]
        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var Companies = await commonService.GetCompanies();
                if (Companies == null)
                {
                    return NotFound();
                }

                return Ok(Companies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetService()
        {
            try
            {
                var Services = await commonService.GetService();
                if (Services == null)
                {
                    return NotFound();
                }

                return Ok(Services);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("SearchService")]
        public async Task<IActionResult> SearchService(string query)
        {
            try
            {
                var SearchResult = await commonService.SearchService(query);
                if (SearchResult == null)
                {
                    return NotFound();
                }

                return Ok(SearchResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("SearchCompany")]
        public async Task<IActionResult> SearchCompany(string query)
        {
            try
            {
                var SearchResult = await commonService.SearchCompany(query);
                if (SearchResult == null)
                {
                    return NotFound();
                }

                return Ok(SearchResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string Sender,string Receiver)
        {
            try
            {
                var ResultMessages = await commonService.GetMessages(Sender,Receiver);
                if (ResultMessages == null)
                {
                    return NotFound();
                }

                return Ok(ResultMessages);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}