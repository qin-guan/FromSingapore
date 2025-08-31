using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql")
    .WithPhpMyAdmin()
    .AddDatabase("from-sg");

var webApi = builder.AddProject<FromSingapore_WebApi>("webapi")
    .WithReference(db);

var webApp = builder.AddNpmApp("webapp", "../FromSingapore.WebApp", "dev")
    .WithReference(webApi);

builder.Build().Run();