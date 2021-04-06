using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using MiyGarden.Service.Interfaces;
using MiyGarden.Service.MiyExtensions;

namespace MiyGarden.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeetCodeController : ControllerBase
    {
        private readonly ILogger<LeetCodeController> _logger;

        public LeetCodeController(ILogger<LeetCodeController> logger)
        {
            _logger = logger;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Get(int number)
        {
            string[] result;
            var leetCode = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblyList()
                .SelectMany(x => x.GetExportedTypes().Where(y => typeof(ILeetCode).IsAssignableFrom(y) && !y.IsInterface))
                .Select(p => (ILeetCode)Activator.CreateInstance(p))
                .FirstOrDefault(x => x.Number == number);
            if (leetCode != null) result = leetCode.Main();
            else result = new string[] { "尚無此題號" };
            return Ok(result);
        }
    }
}
