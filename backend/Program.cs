using backend.Models;
using backend.Services;
using backend.Enpoints;
using Microsoft.EntityFrameworkCore;
using backend.Hubs;

var builder = WebApplication.CreateBuilder(args);

const string connectionString =
    "Host=dpg-d48hk84hg0os7389s7m0-a.oregon-postgres.render.com;" +
    "Port=5432;" +
    "Database=portalexpodb_3rl5;" +
    "Username=portalexpo;" +
    "Password=O3d2EarvBsXDLAI27fQvtKaeMy41BsOQ;" +
    "SSL Mode=Require;" +
    "Trust Server Certificate=true;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddSignalR();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");

// Endpoints

app.MapHub<FeedbackHub>("/feedbackHub");

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

// Mapeamento dos endpoints criados
app.MapAreaEnpoints();
app.MapFeedbackEndpoints();
app.MapExpositanteEndpoints();

app.Run();
