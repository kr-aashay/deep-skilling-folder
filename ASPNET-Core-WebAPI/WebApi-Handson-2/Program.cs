// Program.cs — Entry point with Swagger configured
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// Dependency Injection
// -------------------------------------------------------
builder.Services.AddControllers();

// AddSwaggerGen — registers Swagger generator with API metadata
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Swagger Demo",
        Version     = "v1",
        Description = "Web API with Swagger integration",
        TermsOfService = new Uri("https://www.example.com"),
        Contact = new OpenApiContact
        {
            Name  = "John Doe",
            Email = "john@xyzmail.com",
            Url   = new Uri("https://www.example.com")
        },
        License = new OpenApiLicense
        {
            Name = "License Terms",
            Url  = new Uri("https://www.example.com")
        }
    });
});

var app = builder.Build();

// -------------------------------------------------------
// Middleware pipeline
// -------------------------------------------------------

// UseSwagger — generates the swagger.json at /swagger/v1/swagger.json
app.UseSwagger();

// UseSwaggerUI — serves the Swagger UI page
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
    c.RoutePrefix = "swagger"; // Access at: http://localhost:[port]/swagger
});

app.UseAuthorization();
app.MapControllers();

app.Run();
