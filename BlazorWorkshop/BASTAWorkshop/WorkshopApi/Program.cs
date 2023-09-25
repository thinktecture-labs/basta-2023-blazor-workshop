using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
