using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM_AseeInternship.DataBase;
using PFM_AseeInternship.DataBase.Repositories;
using PFM_AseeInternship.DataBase.Repositories.Implementation;
using PFM_AseeInternship.Services;
using PFM_AseeInternship.Services.Implementations;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TransactionService, TransactionServiceImplementation>();
builder.Services.AddScoped<TransactionRepository, TransactionRepositoryImp>();

builder.Services.AddScoped<CategoryService, CategoryServiceImplementation>();
builder.Services.AddScoped<CategoryRepository, CategoryRepositoryImp>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase) );
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TransactionDbContext>(opt =>
{
    opt.UseNpgsql(CreateConnectionString(builder.Configuration));
});

/*
builder.Services.AddDbContext<CategoryDbContext>(opt =>
{
    opt.UseNpgsql(CreateConnectionString(builder.Configuration));
});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
    //scope.ServiceProvider.GetRequiredService<TransactionDbContext>().Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

string CreateConnectionString(IConfiguration configuration)
{
    var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
    var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
    var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "transactions";
    var host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
    var port = (Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432");

    var connBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = int.Parse(port),
        Username = username,
        Password = password,
        Database = databaseName,
        Pooling = true
    };

    return connBuilder.ConnectionString;
}

/*
string CreateConnectionString2(IConfiguration configuration)
{
    var username = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
    var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
    var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "configurations";
    var host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
    var port = (Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5431");

    var connBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = int.Parse(port),
        Username = username,
        Password = password,
        Database = databaseName,
        Pooling = true
    };

    return connBuilder.ConnectionString;
}

*/