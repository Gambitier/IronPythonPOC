using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronPythonPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            return Ok(new
            {
                status = 200,
                message = "success",
                data = new TestPythonActivity().TestPythonCode()
            });
        }
    }
}
