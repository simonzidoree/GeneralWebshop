using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Webshop.Core.ApplicationService;
using Webshop.Core.ApplicationService.Services;
using Webshop.Core.DomainService;
using Webshop.Infrastructure.Data;
using Webshop.Infrastructure.Data.RepositoriesSQL;
using Webshop.RESTAPI.Helpers;
using Webshop.RESTAPI.Helpers.HelperInterfaces;

namespace Webshop.RESTAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            _cfg = builder.Build();
        }

        private IConfiguration _cfg { get; }

        private IHostingEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Create a byte array with random values. This byte array is used
            // to generate a key for signing JWT tokens.
            var secretBytes = new byte[40];
            var rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(15) //15 minute tolerance for the expiration date
                };
            });

            if (_env.IsDevelopment())
            {
                services.AddDbContext<WebshopContext>(
                    opt => opt.UseSqlite("Data Source=WebshopApp.db")
                );
            }
            else if (_env.IsProduction())
            {
                services.AddDbContext<WebshopContext>(opt =>
                    opt.UseSqlServer(_cfg.GetConnectionString("MS_TableConnectionString")));
            }

            // Register repositories for dependency injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Register the AuthenticationHelper in the helpers folder for dependency
            // injection. It must be registered as a singleton service. The AuthenticationHelper
            // is instantiated with a parameter. The parameter is the previously created
            // "secretBytes" array, which is used to generate a key for signing JWT tokens,
            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowSpecificOrigins",
                    builder => builder
                        .WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<WebshopContext>();
                    DBInitializer.SeedDB(ctx);
                }
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<WebshopContext>();
                    ctx.Database.EnsureCreated();
                }

                app.UseHsts();
            }

            // Use authentication
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}