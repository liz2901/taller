using System.Net;
using Encuba.Product.Api.Dtos.ProductRequests;
using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Encuba.Product.Api.Controllers;

[ApiController]
[Route("product")]
public class ProductController(ILogger<ProductController> logger, ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(bool))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    public async Task<IActionResult> Create(CreateProductRequest request)
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

        return CreatedAtAction(nameof(Create), response);
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(bool))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("{productId:guid}")]
    public async Task<IActionResult> Update(Guid productId, UpdateProductRequest request)
    {
        var command = request.ToApplicationRequest(productId);

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

    [HttpDelete]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(bool))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("{productId:guid}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var request = new DeleteProductRequest();
        var command = request.ToApplicationRequest(productId);

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
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(ProductResponse))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    [Route("{productId:guid}")]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var request = new ReadProductRequest();
        var query = request.ToApplicationRequest(productId);

        logger.LogInformation(
            "----- Sending query: {QueryName} {@Query})",
            nameof(query),
            query);

        var response = await sender.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response.EntityErrorResponse);
        }

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Produces(typeof(ProductResponse))]
    [ProducesErrorResponseType(typeof(EntityErrorResponse))]
    public async Task<IActionResult> GetAll()
    {
        var request = new ReadProductsRequest();
        var query = request.ToApplicationRequest();

        logger.LogInformation(
            "----- Sending query: {QueryName} {@Query})",
            nameof(query),
            query);

        var response = await sender.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response.EntityErrorResponse);
        }

        return Ok(response);
    }
}