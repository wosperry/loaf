using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("greeting")]
    public class GreetingController:ControllerBase
    {
        [HttpGet]
        public string Hello() => "Hello Perry";
    }
}
