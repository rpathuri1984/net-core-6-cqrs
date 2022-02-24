using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RYB.MediatR.Requests;

namespace RYB.api.Controllers;

[RYBAuthorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediatR;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediatR, ILogger<AccountController> logger)
    {
        _mediatR = mediatR;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate()
    {
        return Ok(await _mediatR.Send(new GetUserTokenQuery("")));
    }


}

