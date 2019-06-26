using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeWebSample.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BGController : ControllerBase
    {
        public IActionResult Get()
        {
            Task.Run(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("Compelte");
            });
            return Content("Ok");
        }
    }
}