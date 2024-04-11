using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthController : BaseApiController
{
    private readonly IJwtProvider _jwtProvider;
    private readonly List<string> _validRoles = new List<string> { "Admin", "Seguridad", "Cliente" };
    public AuthController(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    [HttpGet("generate-token")]
    [AllowAnonymous]
    public IActionResult Generate([FromQuery] IEnumerable<string> roles)
    {

        if (roles == null || !roles.Any() || !roles.All(role => _validRoles.Contains(role)))
        {
            return NotFound("Se debe proporcionar uno o más roles válidos");
        }
        //if (roles == null || !roles.Any())
        //{
        //    return BadRequest("Se deben proporcionar al menos un rol.");
        //}

        string token = _jwtProvider.Generate(roles);

        return Ok(token);
    }

    [HttpGet("protected-endpoint")]
    [Authorize]
    public IActionResult ProtectedEndpoint()
    {

        return Ok("Esto es un endpoint protegido");
    }

    [HttpGet("protected-endpoint-seguridad")]
    [Authorize(Roles = "Securidad")]
    public IActionResult ProtectedEndpoint2()
    {

        return Ok("Esto solo puede acceder un miembro de Seguridad");
    }

    [HttpGet("protected-endpoint-admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult ProtectedEndpoint3()
    {

        return Ok("Esto solo puede acceder un Administrador");
    }

    [HttpGet("protected-endpoint-ambos")]
    [Authorize(Roles = "Admin, Securidad")]
    public IActionResult ProtectedEndpoint4()
    {

        return Ok("Esto endpoint pueden ver Seguridad y Admin");
    }

    [HttpGet("protected-endpoint-clientes")]
    [Authorize(Roles = "Cliente")]
    public IActionResult ProtectedEndpoint5()
    {

        return Ok("Esto endpoint solo pueden ver Cliente");
    }


}