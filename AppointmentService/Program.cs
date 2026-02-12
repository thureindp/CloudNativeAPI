// Ensure the Swashbuckle.AspNetCore NuGet package is installed in your project.  
// You can install it using the following command in the terminal:  
// dotnet add package Swashbuckle.AspNetCore  
//using Microsoft.Extensions.DependencyInjection;
//using Swashbuckle.AspNetCore.SwaggerGen; // This requires the Swashbuckle.AspNetCore package to be installed.
using AppointmentService.Data;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    //app.MapOpenApi();
    //app.UseSwagger();
    //app.UseSwaggerUI();


    // Generates /openapi/v1.json by default
    app.MapOpenApi();

    // Swagger UI at /swagger pointing to the built-in OpenAPI JSON
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });


//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//app.Run();
app.Run("http://0.0.0.0:8080");


