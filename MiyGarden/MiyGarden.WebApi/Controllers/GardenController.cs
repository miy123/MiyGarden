using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiyGarden.Service.Extensions;
using MiyGarden.Service.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        [Route("Test")]
        [HttpGet]
        public async Task<string> Test()
        {
            var st = new StringBuilder();
            st.AppendLine("MainI" + Environment.CurrentManagedThreadId);
            var t = TestAsync(st);
            for (var i = 0; i < 99999; i++)
            {
                st.Append("Main" + i);
            }
            await t;
            st.AppendLine("Main" + Environment.CurrentManagedThreadId);

            return st.ToString();
        }

        private async Task TestAsync(StringBuilder st)
        {
            st.AppendLine("Tettt" + Environment.CurrentManagedThreadId);
            await Task.Delay(500);
            for (var i = 0; i < 2; i++)
            {
                st.Append("Tettt" + i);
            }
            st.AppendLine("Tettt" + Environment.CurrentManagedThreadId);
        }
    }
}
