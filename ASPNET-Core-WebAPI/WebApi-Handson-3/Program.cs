using Microsoft.OpenApi.Models;
using WebApiHandson3.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register CustomExceptionFilter for DI (needed for ServiceFilter)
builder.Services.AddScoped<CustomExceptionFilter>();

// Swagger setup
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "WebApi Handson 3",
        Version     = "v1",
        Description = "Custom model, Auth filter, Exception filter"
    });

    // Allow passing Authorization header in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.ApiKey,
        In           = ParameterLocation.Header,
        Description  = "Enter: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi Handson 3");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

app.Run();
