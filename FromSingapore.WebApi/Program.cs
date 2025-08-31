using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using FromSingapore.Core.Context;
using Microsoft.EntityFrameworkCore;

var bld = WebApplication.CreateBuilder(args);
var connectionString = bld.Configuration.GetConnectionString("from-sg");

bld.Services.AddDbContext<AppDbContext>(
    (options) =>
    {
        if (bld.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        }

        options.UseMySql(
            connectionString,
            new MySqlServerVersion(MySqlServerVersion.LatestSupportedServerVersion),
            mysqlOptions =>
            {
                mysqlOptions.MigrationsAssembly("FromSingapore.Migrations");
                mysqlOptions.EnableRetryOnFailure();
            }
        );
    }
);

bld.Services
    .SwaggerDocument(o => o.UseOneOfForPolymorphism = true)
    .AddFastEndpoints();

var app = bld.Build();
app.UseFastEndpoints()
    .UseSwaggerGen();
app.Run();

public class Apple : Fruit
{
    public string Foo { get; set; }
}

public class Orange : Fruit
{
    public string Bar { get; set; }
}

[
    JsonPolymorphic(TypeDiscriminatorPropertyName = "_t"),
    JsonDerivedType(typeof(Apple), "a"),
    JsonDerivedType(typeof(Orange), "o")
]
public class Fruit
{
    public string Baz { get; set; }
}

sealed class MyEndpoint : EndpointWithoutRequest<Fruit>
{
    public override void Configure()
    {
        Post("/api/fruit");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        await SendNotFoundAsync();
    }
}

// using FastEndpoints;
// using FastEndpoints.ClientGen.Kiota;
// using FastEndpoints.Swagger;
// using FromSingapore.Core.Context;
// using FromSingapore.Core.Entities;
// using FromSingapore.WebApi.Extensions;
// using FromSingapore.WebApi.Options;
// using Kiota.Builder;
// using Microsoft.EntityFrameworkCore;
// using Stripe;
//
// var builder = WebApplication.CreateBuilder(args);
//
// StripeConfiguration.ApiKey = builder.Configuration
//     .GetRequiredSection("Stripe")
//     .GetValue<string>("ApiKey");
//
// builder.Services.AddOptions<StripeOptions>()
//     .Bind(builder.Configuration.GetRequiredSection("Stripe"))
//     .Validate((o) => !string.IsNullOrWhiteSpace(o.ApiUri))
//     .ValidateOnStart();
//
// builder.Services.AddCors(options => { options.AddDefaultPolicy(policy => { }); });
// builder.Services.SwaggerDocument(options =>
// {
//     options.UseOneOfForPolymorphism = true;
//     options.DocumentSettings = s => { s.DocumentName = "v1"; };
// }).AddFastEndpoints();
//
// #region Database
//
// var connectionString = builder.Configuration.GetConnectionString("from-sg");
//
// builder.Services.AddDbContext<AppDbContext>(
//     (options) =>
//     {
//         if (builder.Environment.IsDevelopment())
//         {
//             options.EnableSensitiveDataLogging();
//             options.EnableDetailedErrors();
//         }
//
//         options.UseMySql(
//             connectionString,
//             new MySqlServerVersion(MySqlServerVersion.LatestSupportedServerVersion),
//             mysqlOptions =>
//             {
//                 mysqlOptions.MigrationsAssembly("FromSingapore.Migrations");
//                 mysqlOptions.EnableRetryOnFailure();
//             }
//         );
//     }
// );
//
// #endregion
//
// #region Auth
//
// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();
//
// builder.Services
//     .AddIdentityApiEndpoints<AppUser>()
//     .AddEntityFrameworkStores<AppDbContext>();
//
// #endregion
//
// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseCors(options =>
//     {
//         options
//             .WithOrigins(
//                 "http://localhost:3000",
//                 "https://localhost:3000"
//             )
//             .AllowCredentials()
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
//
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// if (app.Environment.IsProduction())
// {
//     app.UseHttpsRedirection();
//     app.UseCors(options =>
//     {
//         options
//             .WithOrigins(
//                 "https://from.sg"
//             )
//             .AllowCredentials()
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// }
//
// app.UseFastEndpoints();
//
// app.MapCustomIdentityApi<AppUser>();
//
// await app.GenerateApiClientsAndExitAsync(c =>
// {
//     c.SwaggerDocumentName = "v1";
//     c.Language = GenerationLanguage.TypeScript;
//     c.CleanOutput = true;
//     c.OutputPath = Path.Combine(app.Environment.ContentRootPath, "../FromSingapore.WebApp", "api");
// });
//
// await using var scope = app.Services.CreateAsyncScope();
// var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
// await db.Database.MigrateAsync();
//
// app.Run();