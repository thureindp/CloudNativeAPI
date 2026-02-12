using Microsoft.AspNetCore.OpenApi;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
//builder.Services.AddSingleton<IConnectionMultiplexer>(
//    ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var options = ConfigurationOptions.Parse("redis:6379");

    options.AbortOnConnectFail = false;
    return ConnectionMultiplexer.Connect(options);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
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
