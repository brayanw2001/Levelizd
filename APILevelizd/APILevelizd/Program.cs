using APILevelizd.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
//string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
string mySqlConnection = $"{builder.Configuration.GetConnectionString("DefaultConnection")}{dbPassword}";   //string de conex�o ao banco de dados


builder.Services.AddDbContext<AppDbContext>(options =>
                              options.UseMySql(mySqlConnection,
                              ServerVersion.AutoDetect(mySqlConnection)));

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

app.Run();
