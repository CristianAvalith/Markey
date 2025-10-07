using Microsoft.OpenApi.Models;

namespace Markey.Server.Config;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Información general de la API
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Markey API",
                Version = "1.0.0", // <-- Change to a valid API version (not OpenAPI version)
                Description = "Markey API proporciona funcionalidades para gestión de usuarios, autenticación, registro, actualización de perfil y recuperación de contraseñas.",
                Contact = new OpenApiContact
                {
                    Name = "Equipo Markey",
                    Email = "soporte@markey.com",
                    Url = new Uri("https://markey.com")
                }
            });

            // Activar anotaciones [SwaggerOperation]
            c.EnableAnnotations();

            // Seguridad JWT Bearer
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header usando Bearer scheme. Ejemplo: 'Authorization: Bearer {token}'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Si quieres usar comentarios XML (para resaltar descripciones de clases y métodos)
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.CustomSchemaIds(type => type.FullName);
            c.DescribeAllParametersInCamelCase();
        });
    }
}