using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Utils;
using Microsoft.AspNetCore.Authorization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        builder.Configuration.Bind("Oidc", options);
        options.RequireHttpsMetadata = false; // NOT DO THIS IN PRODUCTION!
        options.RefreshOnIssuerKeyNotFound = true;
    });

builder.Services.AddAuthorization(config =>
    {
        config.AddPolicy("api", policy =>
        {
            policy.RequireAuthenticatedUser();
        });
    }
);

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(config => config.AddDefaultPolicy(policy =>
{
    policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(origin =>
            origin.StartsWith("https://localhost", StringComparison.InvariantCultureIgnoreCase))
        ;
}));

builder.Services.AddDbContext<ConferencesDbContext>(
    options => options.UseInMemoryDatabase(databaseName: "ConfTool"));

var app = builder.Build();

DataGenerator.Initialize(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
