

using Microsoft.AspNetCore.Mvc;

namespace RelayArchive.Api.Controllers.V1;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly ILogger<RelayInfosController> logger;

    public HomeController(ILogger<RelayInfosController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public ActionResult<string> CheckHealth()
    {
        return "Working fine";
    }
}