using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM_AseeInternship.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TransactionDbContext>(opt =>
{
    opt.UseNpgsql(CreateConnectionString(builder.Configuration));
});

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

string CreateConnectionString(IConfiguration configuration)
{
    var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
    var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
    var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
    var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
    var port = (int.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT")));

    var connBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = port,
        Username = username,    
        Password = password,
        Database = databaseName,
        Pooling = true
    };

    return connBuilder.ConnectionString;
}
