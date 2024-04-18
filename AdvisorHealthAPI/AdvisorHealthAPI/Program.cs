using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Routes;
using AdvisorHealthAPI.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Db Context
builder.Services.AddScoped<AdvisorsDbContext>();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining(typeof(AdvisorValidator));

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
app.AddRoutesAdvisors();


app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }