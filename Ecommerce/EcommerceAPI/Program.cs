using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DockerConnection"));
builder.Services.AddTransient<SeedDb>();

var app = builder.Build();

SeedData(app);
void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (IServiceScope? scope = scopeFactory!.CreateScope())
    {
        SeedDb service=scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x=> x.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin=>true));

app.Run();
