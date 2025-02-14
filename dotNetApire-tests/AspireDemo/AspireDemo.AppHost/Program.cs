var builder = DistributedApplication.CreateBuilder(args);

// Add Redis
var redis = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice");

builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(redis);

builder.Build().Run();