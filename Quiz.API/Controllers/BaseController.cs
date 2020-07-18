using Microsoft.AspNetCore.Mvc;

namespace Quiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}