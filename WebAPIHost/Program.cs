using Application;
using Application.Interfaces;
using Infrastructure;
using Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer("Data Source=localhost;Persist Security Info=True;User ID=user;Password=Caneca1012;Encrypt=False"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(Service<>));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:8080") // Replace with your Vue app's URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Access-Control-Allow-Origin"); ;
    });
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<DataUpsertJob>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIHost", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIHost v1");
    });
}

// Use CORS
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();