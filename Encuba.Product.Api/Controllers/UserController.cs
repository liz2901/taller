using System.Net;
using  Encuba.Product.Api.Dtos.UserRequests;
using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace  Encuba.Product.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController(
    ILogger<UserController> logger,
    ISender sender) : ControllerBase
{
    #region Public methods

        /// <summary>
        /// Create a new manager, linking it with programs and profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /user
        ///     {
        ///         "username": "test",
        ///         "UserType": "test@test.com"
        ///         "FirstName": "test@test.com"
        ///         "SecondName": "test@test.com"
        ///         "FirstLastName": "test@test.com"
        ///         "SecondLastName": "test@test.com"
        ///         "Password": "test@test.com"
        ///         "Email": "test@test.com"
        ///         "AcceptedTermsAndCondition": "test@test.com"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(ObjectIdResponse))]
        [ProducesErrorResponseType(typeof(EntityErrorResponse))]
        public async Task<IActionResult> CreateManagerUser([FromBody] CreateUserRequest request)
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

            return CreatedAtAction(nameof(CreateManagerUser), response.Value);
        }
        
        #endregion
}