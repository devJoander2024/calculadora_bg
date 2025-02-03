using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpClient<SoapClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // Permite solicitudes desde cualquier origen
              .AllowAnyHeader()  // Permite cualquier encabezado
              .AllowAnyMethod(); // Permite cualquier mtodo HTTP (GET, POST, PUT, DELETE, etc.)
    });
});
//builder.WebHost.UseUrls("http://0.0.0.0:8082"); // Esto permite que la API escuche conexiones desde cualquier IP, incluyendo las externas (como la de tu máquina host).
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors(); // Habilitar CORS para todas las rutas de la API

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
