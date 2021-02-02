using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Api.CalculoJuros.Controllers
{
    [ApiController]
    [Route("showmethecode")]
    public class ShowTheCodeController : ControllerBase
    {
        private readonly ILogger<ShowTheCodeController> _logger;
        private readonly string urlGit = ""; 

        public ShowTheCodeController(ILogger<ShowTheCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return urlGit;
        }
    }
}
