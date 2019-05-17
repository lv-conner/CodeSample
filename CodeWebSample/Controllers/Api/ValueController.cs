using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWebSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeWebSample.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly IRepository _repository;
        public ValueController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Get()
        {
            _repository.Add("tim lv");
            return Content(_repository.GetType().FullName);
        }
    }
}