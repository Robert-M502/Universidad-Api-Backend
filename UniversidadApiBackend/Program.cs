// 1. Using para tabajar con EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversidadApiBackend.DataAccess;
using UniversidadApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. Conexion con la base de datos SQL Server
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Agregar contexto
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 7. Agregar servicio o JWT autorizaci�n
// TODO:
//builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// 4. A�adir los servicios (carpeta de los servicios)
builder.Services.AddScoped<IStudentsService, StudentsService>();

// TODO: A�adir el resto de servicios 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 8. TODO: Configuraci�n Swagger para que tenga en cuenta de la autorizaci�n
builder.Services.AddSwaggerGen();

// 5. Habilitar el CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

// 6. Indicarle que nuestra aplicaci�n haga uso de CORS
app.UseCors("CorsPolicy");

app.Run();
