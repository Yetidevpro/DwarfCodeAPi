using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DwarfCodeData;
//git@github.com:Yetidevpro/DwarfCodeApi.git
var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la base de datos para usar la cadena de conexi�n directamente
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Usar la cadena de conexi�n del archivo appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});



// Agregar servicios de controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Configuraci�n para Swagger (documentaci�n y testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Permitir cualquier origen
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");



/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();

// Activar CORS
app.UseCors("AllowSpecificOrigin");


/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Aseg�rate de que las migraciones se apliquen
}*/


app.MapControllers();

app.Run();
