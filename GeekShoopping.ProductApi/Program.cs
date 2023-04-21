using AutoMapper;
using GeekShoopping.ProductApi.Config;
using GeekShoopping.ProductApi.Model.Context;
using GeekShoopping.ProductApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];
builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(connection,
        new MySqlServerVersion(
                new Version(8, 0, 32))));
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
