using GeekShopping.Email.MessageConsumer;
using GeekShopping.Email.Model.Context;
using GeekShopping.Email.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["MySQlConnection:MySQlConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options => options.
    UseMySql(connection,
            new MySqlServerVersion(
                new Version(8, 0, 32))));

var builderSql = new DbContextOptionsBuilder<MySqlContext>();
builderSql.UseMySql(connection,
            new MySqlServerVersion(
                new Version(8, 0, 32)));

builder.Services.AddSingleton(new EmailRepository(builderSql.Options));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddHostedService<RabbitMQPaymentConsumer>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.Email", Version = "v1" });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
