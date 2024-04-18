using AdvisorHealthAPI.Data;
using AdvisorHealthAPI.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AdvisorsDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routes
app.AddRoutesAdvisors();


app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }