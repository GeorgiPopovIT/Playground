using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ_Demo.Data;
using RabbitMQ_Demo.Models;
using RabbitMQ_Demo.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrdersDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapPost("orders", async (OrdersDbContext _context, IMessageProducer _messagePublisher, [FromBody]OrderDTO orderDto) =>
{
    Order order = new()
    {
        ProductName = orderDto.ProductName,
        Price = orderDto.Price,
        Quantity = orderDto.Quantity
    };

    //_context.Orders.Add(order);
    //await _context.SaveChangesAsync();
   // _messagePublisher.SendMessage<Order>(order);
    _messagePublisher.PublishAndSubscripe(order);

    return Results.Ok(new { id = order.Id });
});

app.Run();