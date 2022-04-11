using Metronik.Data;
using Metronik.DTOs;
using Metronik.Enteties;
using Metronik.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metronik.Controllers;



[ApiController]
[Route("api/v2/")]
public class AuthenticationController : ControllerBase
{
    private DataContext _context;
    private readonly TokenService _tokenService;

    public AuthenticationController(DataContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    [HttpPost("login")]
    public async Task<ActionResult<OmsDto>> GetToken(OmsDto omsDto)
    {
        var res = _context.Oms.FirstOrDefault(x => x.omsId == omsDto.OmsId);
        if (res != null)
            return new OmsDto
            {
                Id = res.Id.ToString(),
                ClientToken = await _tokenService.CreateToken(omsDto),
                OmsId = res.omsId
            };
        return BadRequest("Unknown OMS Id.");
    }


}