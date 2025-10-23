using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace server.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddSwaggerAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API para o PNI",
                    Version = "v1"
                });

                // Configuração do esquema JWT
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Digite 'Cole APENAS o token JWT aqui (sem 'Bearer ')",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                // Adiciona o requisito de segurança com referência correta
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
                        new string[] {}
                    }
                });
            });


            return services;
        }

        public static IServiceCollection AddAuthenticationSuport(this IServiceCollection services)
        {
            var key = "wedejdn983843ybrh21671tegbhd65627121bue12y6t8712ebg1y5e6387hb1budvt1563";

            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(op =>
              {
                  op.RequireHttpsMetadata = false;
                  op.SaveToken = true;
                  op.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                      ValidateIssuer = false,
                      ValidIssuer = "me",
                      ValidateAudience = false,
                      ValidAudience = "you",
                      ValidateLifetime = true,
                      RoleClaimType = ClaimTypes.Role,
                      // Permitir pequeno skew para evitar rejeições imediatas em dev
                      ClockSkew = TimeSpan.FromMinutes(1)
                  };

                  // Adiciona logging para falhas de autenticação (ajuda a diagnosticar "invalid_token")
                  op.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = ctx =>
                      {
                          // opcional: log do header recebido
                          if (ctx.Request.Headers.ContainsKey("Authorization"))
                          {
                              Console.WriteLine($"Authorization header: {ctx.Request.Headers["Authorization"]}");
                          }
                          return Task.CompletedTask;
                      },
                      OnAuthenticationFailed = ctx =>
                      {
                          Console.WriteLine($"JWT Authentication failed: {ctx.Exception?.Message}");
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddAuthorization(op =>
            {
                op.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });
            return services;
        }
    }
}
