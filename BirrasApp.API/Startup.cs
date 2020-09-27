using AutoMapper;
using BirrasApp.External.Services;
using BirrasApp.External.Services.Interfaces;
using BirrasApp.Mappers;
using BirrasApp.Repositories;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Services;
using BirrasApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace BirrasApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BirrasAppContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(ModelsProfile));

            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IMeetupsService, MeetupsService>();
            services.AddHttpClient<IWeatherService, OpenWeatherService>();
            services.AddTransient<IUserMeetupsService, UserMeetupsService>();
            services.AddTransient<IUserRequestToMeetUpService, UserRequestToMeetUpService>();

            // Repositories  
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IMeetupsRepository, MeetupsRepository>();
            services.AddTransient<IUserMeetupsRepository, UserMeetupsRepository>();
            services.AddTransient<IUserRequestToMeetUpRepository, UserRequestToMeetUpRepository>();            

            // En produccion debe ser mas restrictivo
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }).AddCookie();

            services.AddAuthorization( options => {
                options.AddPolicy("OnlyAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BirraApp API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT requerido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BirraApp V1");
                c.RoutePrefix = string.Empty;
            });

            // En produccion debe ser mas restrictivo
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();            
        }
    }
}
