using Microsoft.OpenApi.Models;
using RYB.api.Middleware;
using RYB.Cryptography;
using RYB.MediatR;
using RYB.Model.AppSettings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "RYB API",
            Version = "v1"
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
           {
             new OpenApiSecurityScheme
             {
               Reference = new OpenApiReference
               {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer",
               }
              },
              new string[] { }
            }
          });
    });


// Add RYB services to the container.
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddRYBMediatR();
builder.Services.AddRYBCryptography();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();
