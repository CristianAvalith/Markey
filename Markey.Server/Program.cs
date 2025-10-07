using FluentValidation;
using Markey.Application.Auth.Login;
using Markey.Application.Auth.Register;
using Markey.Application.Catalog.GetOccupations;
using Markey.Application.User.GetUser;
using Markey.Application.User.List.ListUsers;
using Markey.Application.User.Update;
using Markey.Application.User.UpdatePhoto;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.MessagesManager;
using Markey.Domain.Helper;
using Markey.Domain.Interfaces;
using Markey.Domain.Services;
using Markey.Persistance;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.Interface;
using Markey.Server.Helper;
using Markey.Server.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// --- Servicios, Repositories, Validators ---
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<GetUserRequest>, GetUserRequestValidator>();
builder.Services.AddScoped<IValidator<ListUsersByFiltersRequest>, ListUsersByFiltersRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateUserPhotoRequest>, UpdateUserPhotoRequestValidator>();
builder.Services.AddScoped<IValidator<GetOccupationsRequest>, GetOccupationsRequestValidator>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.ConfigureMessageManager();
builder.Services.AddScoped<Mediator>();

builder.Services.AddJwtAuthentication(builder.Configuration);

// Mediator Handlers
builder.Services.Scan(scan => scan
    .FromAssemblies(typeof(GetOccupationsHandler).Assembly)
    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// DB Context
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MarkeyDatabase"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Markey API",
        Version = "1.0.0",
        Description = "Markey API proporciona funcionalidades para gestión de usuarios.",
    });

    c.EnableAnnotations();

    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// AutoMapper e Identity
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireUppercase = false;
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<MyDbContext>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// SPA Services
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "../Markey.Client/dist";
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Markey API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SPA Configuration solo para desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api") && 
                     !x.Request.Path.Value.StartsWith("/swagger") &&
                     !x.Request.Path.Value.StartsWith("/catalog") &&
                     !x.Request.Path.Value.StartsWith("/user") &&
                     !x.Request.Path.Value.StartsWith("/auth"), builder =>
    {
        builder.UseSpa(spa =>
        {
            spa.Options.SourcePath = "../Markey.Client";
            spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
        });
    });
}


// Migraciones
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var context = services.GetRequiredService<MyDbContext>();
    var logger = loggerFactory.CreateLogger<Program>();

    logger.LogInformation("Iniciando migraciones...");

    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
}

app.Run();
