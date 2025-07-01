using System.Net;
using  Encuba.Product.Api.Dtos.AuthJwtRequests;
using  Encuba.Product.Domain.Dtos;
using  Encuba.Product.Domain.Entities;
using  Encuba.Product.Domain.Seed;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace  Encuba.Product.Api.Controllers;

[ApiController]
public class AuthJwtController(
    ILogger<AuthJwtController> logger,
    ISender sender,
    IOptionsMonitor<JWT> appSettings)
    : ControllerBase
{
    private readonly JWT _appSettings = appSettings.CurrentValue;

    #region Public methods

    /// <summary>
    /// Generates JWT from accessToken
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/auth/jwt
    ///     {
    ///         "AccessToken": "accesstoken",
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [JwtAuthorize(JwtScope.Client)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(JwtResponse))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("/user/auth/jwt")]
    public async Task<IActionResult> CreateToken([FromBody] CreateJwtRequest request)
    {
        var command = request.ToApplicationRequest(_appSettings.Secret!);

        logger.LogInformation(
            "----- Sending command: {CommandName} {@Command})",
            nameof(command),
            command);

        var response = await sender.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest(response.EntityErrorResponse);
        }

        return Ok(response.Value);
    }

    #endregion
}