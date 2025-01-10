using DwarfCodeData;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Necesario para ConfigurationBuilder
using System.IO;  // Necesario para Directory.GetCurrentDirectory()

class Program
{
    static void Main(string[] args)
    {
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json") 
            .Build();

        
        string connectionString = configuration.GetConnectionString("DefaultConnection");

       
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("DwarfAnimeBackend"));

       
        using (var context = new AppDbContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();  
        }
    }
}


