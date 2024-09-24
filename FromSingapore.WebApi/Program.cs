using FastEndpoints;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Swagger;
using FromSingapore.Core.Context;
using FromSingapore.Core.Entities;
using Kiota.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(options => { options.DocumentSettings = s => { s.DocumentName = "v1"; }; });

#region Database

var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        }

        options.UseMySql(
            connectionString,
            new MySqlServerVersion(ServerVersion.AutoDetect(connectionString)),
            mysqlOptions => { mysqlOptions.MigrationsAssembly("FromSingapore.Migrations"); }
        );
    }
);

#endregion

#region Auth

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();

#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseFastEndpoints();

await app.GenerateApiClientsAndExitAsync(c =>
{
    c.SwaggerDocumentName = "v1";
    c.Language = GenerationLanguage.TypeScript;
    c.OutputPath = Path.Combine(app.Environment.ContentRootPath, "../FromSingapore.WebApp", "api");
});

await using var scope = app.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.MigrateAsync();

app.MapIdentityApi<AppUser>();

app.Run();