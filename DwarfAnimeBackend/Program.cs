using Microsoft.EntityFrameworkCore;
using DwarfCodeData;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext al contenedor de servicios
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios de controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Manejo de ciclos de referencia
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        // Si es necesario, también puedes aumentar la profundidad máxima aquí
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Configuración para Swagger (documentación y testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permitir solicitudes desde cualquier origen
              .AllowAnyHeader()  // Permitir cualquier encabezado
              .AllowAnyMethod(); // Permitir cualquier método HTTP (GET, POST, etc.)
    });
});

var app = builder.Build();

// Configuración del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar CORS
app.UseCors("AllowAll"); // Esto permite las solicitudes de cualquier origen

// app.UseAuthentication();
// app.UseAuthorization();

// Ejecutar migraciones si es necesario
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //dbContext.Database.Migrate();  // Descomentar si es necesario aplicar migraciones automáticamente
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Mapear controladores
app.MapControllers();

app.Run();


