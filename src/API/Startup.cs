using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using API.Exceptions;
using API.Users;
using Core;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Core.Pets;
using Core.Organizations;
using Core.Locations;
using Core.Bookings;
using Core.Memberships;

namespace API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCore();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(new string[] { 
                            "https://localhost:3001", 
                            "http://localhost:3000", 
                            "https://localhost:5001", 
                            "http://localhost:5000" });
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                    }
                    ); 
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddDbContext<ApplicationContext>(options => 
                options.UseInMemoryDatabase("App"));

            services.AddDbContext<IdentityContext>(options =>
                options.UseInMemoryDatabase("Identity"));

            services.AddScoped<IPetRepository, PetRepository>();

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IMembershipRepository, MembershipRepository>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options));

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "authCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    context.Response.Headers.Add("X-Authenticated", "false");
                    return Task.CompletedTask;
                };
            });

            if (_env.IsDevelopment())
            {
                // Lax password rules for dev
                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 2;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                });
            }
            else
            {
                services.Configure<IdentityOptions>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                });
            }

            services.AddScoped<IUserService, UserService>();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1", 
                    new OpenApiInfo { Title = "DoggyDaycare API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // This causes stacktrace to show in response
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigins");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoggyDaycare API V1");
            });

        }
    }
}
