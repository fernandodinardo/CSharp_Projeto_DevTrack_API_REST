using DevTrackR.API.Persistence;
using DevTrackR.API.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevTrackRCs");
builder.Services
    .AddDbContext<DevTrackRContext>(o => o.UseSqlServer(connectionString));

/*  Opção caso não tenha o SQL Server instalado na máquina. 
    Utilizaria DataBase em memória.
builder.Services
     .AddDbContext<DevTrackRContext>(o => o.UseInMemoryDatabase("DevTrackRDataBase")); 
*/

// Exemplo para usar o BD em Memória
//builder.Services.AddSingleton<DevTrackRContext>();

builder.Services.AddScoped<IPackageRepository, PackageRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
