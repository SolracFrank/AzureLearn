using System.Text.Json.Serialization;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Web.Conventions;
using Web.FIlters;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

#region Infrastructure

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region Controllers

builder.Services.AddControllers(options => { options.Filters.Add<ValidateModelAttribute>(); })
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });

#endregion

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.Conventions.Add(new VersionByNamespaceConvention());
});

#region CORS

const string corsPolicy = "_allowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy
            .WithOrigins("https://localhost:7295", "https://localhost:5123")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AzureLean API",
        Version = "v1",
        Description = "API for students AZURE connection practice :)",
        Contact = new OpenApiContact
        {
            Name = "Fran Dev",
            Email = ""
        },
        License = new OpenApiLicense
        {
            Name = "Free",
            Url = new Uri("https://github.com/SolracFrank")
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzureLean API v1");
        c.DocumentTitle = "Swagger - AzureLean";
        c.RoutePrefix = string.Empty;
    });

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(corsPolicy);
app.UseMiddleware<ErrorHandlingMiddleware>();

// app.UseAuthentication(); 
// app.UseAuthorization();

app.MapControllers();
app.Run();