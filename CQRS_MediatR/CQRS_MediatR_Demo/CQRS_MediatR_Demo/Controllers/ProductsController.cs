using CQRS_MediatR_Demo.Commands;
using CQRS_MediatR_Demo.Data;
using CQRS_MediatR_Demo.Notifications;
using CQRS_MediatR_Demo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_MediatR_Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
        => this._mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        var productToReturn = await _mediator.Send(new AddProductCommand(product));

        await _mediator.Publish(new ProductAddedNotification(productToReturn));

        return CreatedAtAction("GetProductById", new {id = productToReturn.Id },productToReturn);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }
}
