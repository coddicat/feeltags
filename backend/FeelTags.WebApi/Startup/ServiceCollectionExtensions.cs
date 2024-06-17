using FeelTags.WebApi.ApiServices;
using FeelTags.WebApi.Dal;
using FeelTags.WebApi.Dal.Repositories;
using FeelTags.WebApi.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FeelTags.WebApi.Startup
{
    public static class ServiceCollectionExtensions
    {
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new() {
                    {
                        new ()
                        {
                            Reference = new ()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }});
                //c.DocumentFilter<PrefixSwaggerDocumentFilter>();
            });
        }


        public static IServiceCollection ConfigureSwaggerAndHsts(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ConfigureSwagger(services);
            }
            else
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(60);
                });
            }

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IAccountRepo, AccountRepo>()
                .AddScoped<IAuthApiService, AuthApiService>()
                .AddScoped<IQuestionRepo, QuestionRepo>()
                .AddScoped<IQuestionApiService, QuestionApiService>();
        }

        public static IServiceCollection ConfigureSettings(this IServiceCollection services)
        {            
            services.AddOptions<TokenServiceSettings>().BindConfiguration(TokenServiceSettings.SECTION)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            //services.AddOptions<SecretSettings>().BindConfiguration(SecretSettings.SECTION)
            //    .ValidateDataAnnotations()
            //    .ValidateOnStart();

            return services;
        }

        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<IFeelTagsContext, FeelTagsContext>(options => {
                string? connectionString = configuration.GetConnectionString("FeelTagsDb");
                ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.UseNetTopologySuite())
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine);
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorization(options =>
                {
                    //options.AddPolicy(Policy.RequireUserId, policy =>
                    //    policy.RequireClaim(ClaimTypes.NameIdentifier));
                    //options.AddPolicy(Policy.RequireUserData, policy =>
                    //    policy.RequireClaim(ClaimTypes.UserData));
                    //options.AddPolicy(Policy.RequireHash, policy =>
                    //    policy.RequireClaim(ClaimTypes.Hash));
                })
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => ConfigureJwtToken(configuration, options));

            return services;
        }

        private static void ConfigureJwtToken(IConfiguration configuration, JwtBearerOptions options)
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    //context.Token = context.Request.Headers.Authorization.ToString();
                    //if (string.IsNullOrWhiteSpace(context.Token))
                    //{
                    //    context.Token = context.Request.Cookies["AccessToken"];
                    //}
                    context.Token = context.Request.Cookies["AccessToken"]; //only from cookies
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;
                },
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["Token:Issuer"],
                ValidAudience = configuration["Token:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]!))
            };
        }

    }
}
