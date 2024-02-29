using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Store99.AppContext;
using Store99.Interfaces.Repositories;
using Store99.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ignoramos las referencias
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// agregamos el automapper con esos parametros
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// hacemos la inyección de independencia de repositorio
builder.Services.AddScoped<IShoeRepository, ShoeRepository>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// traemos el db context con las opciones
builder.Services.AddDbContext<DataContext>(options =>
{
    // usamos el sql server de options. dentro obtenemos la default connection que seteamos en appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

string cloudinaryApiKey = null;
string cloudinaryApiSecret = null;
string cloudinaryCloudName = null;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // damos el path para secrets
    builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
    app.UseSwagger();
    app.UseSwaggerUI();
    cloudinaryApiKey = builder.Configuration["CLOUDINARY_API_KEY"];
    cloudinaryApiSecret = builder.Configuration["CLOUDINARY_API_SECRET"];
    cloudinaryCloudName = builder.Configuration["CLOUDINARY_CLOUD_NAME"];
} else
{
    cloudinaryApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
    cloudinaryApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
    cloudinaryCloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
}

if (cloudinaryApiKey == null || cloudinaryApiSecret == null || cloudinaryCloudName == null)
{
    throw new InvalidOperationException("Faltan variables de entorno necesarias para la configuración de Cloudinary.");
}

Account account = new Account(
    cloudinaryCloudName,
    cloudinaryApiKey,
    cloudinaryApiSecret);
Cloudinary cloudinary = new(account);
cloudinary.Api.Secure = true;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
