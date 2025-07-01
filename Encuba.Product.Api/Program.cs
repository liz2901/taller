using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Encuba.Product.Api.Configurations.AutofacConfig;
using Encuba.Product.Domain.Seed;
using Encuba.Product.Infrastructure;
using Encuba.Product.Infrastructure.Middlewares;
using Encuba.Product.Infrastructure.Redis;
using Encuba.Product.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Load the configuration from appsettings.json and environment variables
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("Security");

builder.Services.AddDbContext<SecurityDbContext>(options =>
    options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Encuba Api",
        Description = "Api rest microservice for security",
        Contact = new OpenApiContact
        {
            Name = "Alex Asitimbay",
            Email = "alex.asitimbay@outlook.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//JWT config
builder.Services.AddOptions<JwtAuthenticationSettings>()
    .Bind(configuration.GetSection("JWT"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(opt => opt.Filters.Add(typeof(JwtAuthorizationActionFilter)))
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ContractResolver = new PrivateSetterContractResolver());

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepositoryModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new MediatorModule()));

builder.Services.AddOptions<JWT>()
    .BindConfiguration("JWT")
    .ValidateDataAnnotations();

builder.Services.AddAuthentication("JWT")
    .AddScheme<JwtAuthenticationSchemaOptions, JwtAuthenticationHandler>("JWT", null);

builder.Services.AddSingleton<RedisConfiguration>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")  // La dirección que deseas permitir
                .AllowAnyMethod()                       // Permitir cualquier método (GET, POST, etc.)
                .AllowAnyHeader()                       // Permitir cualquier encabezado
                .AllowCredentials();                     // Permitir credenciales si es necesario
        });
});
//Add serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .MinimumLevel.Verbose()
    .Enrich.WithProperty("SecurityDbContext", "Encuba.Product.Api")
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog(logger);

var app = builder.Build();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();