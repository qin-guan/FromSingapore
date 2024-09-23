var builder = DistributedApplication.CreateBuilder(args);

builder.AddNpmApp("webapp", "../FromSingapore.WebApp", "dev");

builder.Build().Run();