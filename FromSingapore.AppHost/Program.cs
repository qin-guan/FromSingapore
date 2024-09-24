using System.Runtime.InteropServices;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddContainer("mysql", "mysql")
    .WithVolume("mysql-fromsingapore", "/var/lib/mysql")
    .WithEnvironment("MYSQL_ROOT_PASSWORD", "sa");

mysql = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? mysql.WithContainerRuntimeArgs("--net=host")
    : mysql.WithEndpoint(3306, 3306);

var adminer = builder.AddContainer("adminer", "adminer");

adminer = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? adminer.WithContainerRuntimeArgs("--net=host")
    : adminer.WithEndpoint(8080, 8080);

var webApi = builder.AddProject<FromSingapore_WebApi>("webapi");

var webApp = builder.AddNpmApp("webapp", "../FromSingapore.WebApp", "dev")
    .WithReference(webApi);

builder.Build().Run();