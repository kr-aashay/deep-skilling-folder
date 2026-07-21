// Program.cs — Entry point and configuration of the Web API
// Equivalent of Startup.cs + WebApiConfig in .NET 4.5

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// Dependency Injection (replaces Startup.cs ConfigureServices)
// -------------------------------------------------------
builder.Services.AddControllers();  // Registers all controllers

var app = builder.Build();

// -------------------------------------------------------
// Middleware pipeline (replaces Startup.cs Configure)
// -------------------------------------------------------
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();   // Maps routes to controllers

app.Run();
