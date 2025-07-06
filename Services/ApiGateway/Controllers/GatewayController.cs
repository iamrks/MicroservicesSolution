using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatewayController(IConfiguration config) : ControllerBase
{
    private readonly IConfiguration _config = config;

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(new { Status = "Api Gateway is running", BaseUrl = _config["BaseUrl"] });
    }

    [Authorize]
    [HttpGet("secure-ping")]
    public IActionResult SecurePing()
    {
        return Ok(new
        {
            status = "Secure ping successful",
            user = User.Identity?.Name,
            time = DateTime.UtcNow
        });
    }
}