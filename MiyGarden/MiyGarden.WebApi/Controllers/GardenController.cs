using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiyGarden.Service.Extensions;
using MiyGarden.Service.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace MiyGarden.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GardenController : ControllerBase
    {
        private readonly ILogger<GardenController> _logger;

        public GardenController(ILogger<GardenController> logger)
        {
            _logger = logger;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult LeetCode(int number)
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

        [Route("[action]")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok();
        }
    }
}
