using System.Net;
using  Encuba.Product.Api.Dtos.AuthUserRequests;
using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace  Encuba.Product.Api.Controllers;

[ApiController]
[Route("user/auth")]
public class AuthUserController(
    ILogger<AuthUserController> logger,
    ISender sender
) : ControllerBase
{
    #region Public methods

    /// <summary>
    /// Authenticates a member user
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/auth/login
    ///     {
    ///         "username": "email@email.com | identificationNumber",
    ///         "password": "password"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="ipHeader"></param>
    /// <param name="userAgent"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(PublicAccessTokenResponse))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("login")]
    public async Task<IActionResult> LoginMemberUser([FromBody] LoginUserRequest request,
        [FromHeader(Name = "User-Agent")] string userAgent,
        [FromHeader(Name = "X-Forwarded-For")] string ipHeader)
    {
        var ipAddress = string.IsNullOrEmpty(ipHeader) ? "127.0.0.1" : ipHeader.Split(',')[0];

        var command = request.ToApplicationRequest(userAgent, ipAddress);

        logger.LogInformation(
            "----- Sending command: {CommandName} {@Command})",
            nameof(command),
            command);

        var response = await sender.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest(response.EntityErrorResponse);
        }

        return Ok(response);
    }

    /// <summary>
    /// Generates a new refresh token if the passed is expired, if not returns the same public access token
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/auth/refresh-token
    ///     {
    ///         "refreshToken": "XXX"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(PublicAccessTokenResponse))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshMemberUserToken([FromBody] RefreshTokenRequest request)
    {
        var command = request.ToApplicationRequest();

        logger.LogInformation(
            "----- Sending command: {CommandName} {@Command})",
            nameof(command),
            command);

        var response = await sender.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest(response.EntityErrorResponse);
        }

        return Ok(response);
    }

    #endregion
}