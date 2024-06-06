using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

//builder.Services.AddStackExchangeRedisCache(conf =>
//{
//    conf.
//});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/redis", async ([FromQuery] string name, IConnectionMultiplexer muxer) =>
{
    var _redis = muxer.GetDatabase();
    //await _redis.KeyDeleteAsync("hi");
    var result = await _redis.StringGetAsync("hi");
    if (string.IsNullOrEmpty(result))
    {
        var setTask =  _redis.StringSetAsync("hi", name);
        var expireTask =  _redis.KeyExpireAsync("hi", new TimeSpan(0,0,5));

        await Task.WhenAll(setTask, expireTask);
        result = await _redis.StringGetAsync("hi");
    }


    return TypedResults.Ok(result);
});

app.Run();