using FarmControl.Api.Filters;
using FarmControl.Application;
using FarmControl.Application.Services.AutoMapper;
using FarmControl.Domain.Extension;
using FarmControl.Infrastructure;
using FarmControl.Infrastructure.Migrations;
using HashidsNet;
using MeuLivroDeReceitas.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));


builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration(provider.GetService<IHashids>()));
}).CreateMapper());

builder.Services.AddScoped<AuthenticatedUserAttribute, AuthenticatedUserAttribute>();


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