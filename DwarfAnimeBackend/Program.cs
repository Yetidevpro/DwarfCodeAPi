using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DwarfCodeData;
//git@github.com:Yetidevpro/DwarfCodeApi.git
var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext al contenedor de servicios
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(async connection =>
    {
        var credential = new ManagedIdentityCredential();
        var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));

        var connectionString = "Server=tcp:dwarfanimeprova.database.windows.net,1433;Database=DwarfAnimeProva;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        var sqlConnection = new SqlConnection(connectionString)
        {
            AccessToken = token.Token
        };

        options.UseSqlServer(sqlConnection);
    });
});


// Agregar servicios de controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Configuración para Swagger (documentación y testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://red-wave-088e75903.4.azurestaticapps.net") // URL de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
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
app.UseCors("AllowSpecificOrigin");

// Ejecutar migraciones si es necesario
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //dbContext.Database.Migrate();  // Descomentar si es necesario aplicar migraciones automáticamente
}

app.MapControllers();

app.Run();
