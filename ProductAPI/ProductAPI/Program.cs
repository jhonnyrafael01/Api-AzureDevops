using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductAPI.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ProductAPI",
        Description = "API CRUD de lista de Produtos simples",
        TermsOfService = new Uri("https://www.linkedin.com/in/jhonny-rafael-179138211/"),
        Contact = new OpenApiContact
        {
            Name = "Jhonny Rafael",
            Email = "rafaeljhonny01@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/jhonny-rafael-179138211/")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://github.com/jhonnyrafael01")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
