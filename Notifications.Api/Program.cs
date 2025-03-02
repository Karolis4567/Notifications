using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notifications.Api.JsonConverters;
using Notifications.Application.Interfaces;
using Notifications.Application.Mappings;
using Notifications.Application.Services;
using Notifications.Infrastructure.Data;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(CompanyMappingProfile), typeof(CompanyNotificationMappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<NotificationsAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISharedServices, SharedServices>();
builder.Services.AddScoped<ICompanyServices, CompanyServices>();
builder.Services.AddScoped<ICompanyNotificationService, CompanyNotificationService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter("dd/MM/yyyy", new CultureInfo("en-GB")));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications.Api", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationsAppContext>();
        dbContext.Database.Migrate();
    }    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
