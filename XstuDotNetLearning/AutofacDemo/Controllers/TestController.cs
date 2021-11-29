using AutofacDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutofacDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly IPersonService _personService;
        public TestController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("getTalk")]
        public string GetTalkContent()
        {
            return _personService.Talk();
        }

    }
}