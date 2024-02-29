using Microsoft.EntityFrameworkCore;
using Store99.AppContext;
using Store99.Interfaces;
using Store99.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ignoramos las referencias
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddLogging(logging =>
{
    logging.AddConsole(); // Registrar en la consola
                          // Otros proveedores de registro como AddFile, AddDebug, AddEventLog, etc.
});
// agregamos el automapper con esos parametros
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// hacemos la inyección de independencia
builder.Services.AddScoped<IShoeRepository, ShoeRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// traemos el db context con las opciones
builder.Services.AddDbContext<DataContext>(options =>
{
    // usamos el sql server de options. dentro obtenemos la default connection que seteamos en appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.Run();
