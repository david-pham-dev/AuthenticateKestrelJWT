using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AuthenticateAppBackEnd.Extensions
{
    internal static class AddSwaggerAuth
    {
        internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWTAuthentication",
                    Description = "Enter your JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };
                o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        []
                    }
                };
                o.AddSecurityRequirement(securityRequirement);
            });   
            return services;
        }
    }
}
