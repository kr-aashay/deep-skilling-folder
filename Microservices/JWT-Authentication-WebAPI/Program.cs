using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ── Question 1: JWT Authentication setup ──────────────────────────────────────
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

        // ── Question 4: Handle token expiry and invalid tokens gracefully ────
        options.Events = new JwtBearerEvents
        {
            // Fires when authentication fails (expired, invalid signature, etc.)
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    // Add custom header so client knows the token expired (not just invalid)
                    context.Response.Headers.Append("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },

            // Fires when a request is rejected as unauthorized
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode  = 401;
                context.Response.ContentType = "application/json";
                var message = context.AuthenticateFailure?.Message ?? "Unauthorized. Provide a valid Bearer token.";
                return context.Response.WriteAsync($"{{\"error\": \"{message}\"}}");
            },

            // Fires when user is authenticated but not authorized (wrong role)
            OnForbidden = context =>
            {
                context.Response.StatusCode  = 403;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\": \"Forbidden. You do not have the required role.\"}");
            }
        };
    });

// ── Question 2 & 3: Authorization ────────────────────────────────────────────
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();  // Must come before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();
