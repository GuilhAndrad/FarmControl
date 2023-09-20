using FarmControl.Domain.Extension;
using FarmControl.Infrastructure;
using FarmControl.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

UpdateDataBase();

app.Run();

void UpdateDataBase()
{
    var connection = builder.Configuration.GetConnection();
    var databaseName = builder.Configuration.GetNameDatabase();

    Database.CreateDatabase(connection, databaseName);

    app.MigrateDatabase();
}