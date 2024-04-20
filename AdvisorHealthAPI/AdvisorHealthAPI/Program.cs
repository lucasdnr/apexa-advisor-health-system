using AdvisorHealthAPI.Caching;
using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Routes;
using AdvisorHealthAPI.Validators;
using FluentValidation;

//var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "browser"
});

// Add services to the container.
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Db Context
builder.Services.AddScoped<AdvisorsDbContext>();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining(typeof(AdvisorValidator));

// Caching
// Add services to the container.
//builder.Services.AddSingleton(new LRUCache<string, Advisor>(5));

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// configures application to serve the index.html file from /wwwroot
app.UseDefaultFiles();

// static files
app.UseStaticFiles();

// Routes
//new LRUCache<string, Advisor>(5)
AdvisorsRoutes.AddRoutesAdvisors(app, new LRUCache<string, Advisor>(5));
//app.AddRoutesAdvisors();

// CORS
app.UseCors("MyAllowedOrigins");

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }