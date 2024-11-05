using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration: builder.Configuration["CacheConnection"]));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["CacheConnection"];
    options.InstanceName = "SampleInstance";
});

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
        var setTask = _redis.StringSetAsync("hi", name);
        var expireTask = _redis.KeyExpireAsync("hi", new TimeSpan(0, 0, 40));

        await Task.WhenAll(setTask, expireTask);
        result = await _redis.StringGetAsync("hi");
    }


    return TypedResults.Ok(result);
});

app.MapGet("/redis2", async ([FromQuery] string name, IDistributedCache redis) =>
{
    var result = await redis.GetStringAsync("hi");
    if (string.IsNullOrEmpty(result))
    {
        await redis.SetStringAsync("hi", name);
        result = await redis.GetStringAsync("hi");
    }

    return TypedResults.Ok(result);
});

app.Run();