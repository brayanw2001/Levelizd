using APILevelizd.Context;
using APILevelizd.Repositories.Interfaces;
using APILevelizd.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using APILevelizd.Services.Interfaces;
using APILevelizd.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>              // ignora referência cíclica
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPassword = builder.Configuration["DB_PASSWORD"];
//string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
string mySqlConnection = $"{builder.Configuration.GetConnectionString("DefaultConnection")};pwd={dbPassword}";   //string de conexão ao banco de dados

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFileService, FileService>();
/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});*/


builder.Services.AddDbContext<AppDbContext>(options =>
                              options.UseMySql(mySqlConnection,
                              ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// mapping Uploads folder to Resources folder 
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
//    RequestPath = "/Resources"
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(); // qualquer arquivo dentro de wwwroot se torna acessível via URL

//app.UseCors();

app.Run();
