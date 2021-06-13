using System.Text;
using AutoShop.API.Configurations;
using AutoShop.API.Helpers;
using AutoShop.API.Mapper;
using AutoShop.API.Validators;
using AutoShop.Core.Interfaces.Configurations;
using AutoShop.Core.Interfaces.Managers;
using AutoShop.Core.Interfaces.Repositories;
using AutoShop.Core.Managers;
using AutoShop.Core.Models;
using AutoShop.Data;
using AutoShop.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AutoShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AutoShopContext>(options =>
            {
                options.UseSqlServer(connection, b => b.MigrationsAssembly("AutoShop.API"));
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoShopAPI", Version = "v1" });
                c.IncludeXmlComments(SwaggerHelper.XmlCommentsFilePath);
            });

            services.AddAutoMapper(m => m.AddProfile(new StandartProfile()));

            services.AddSingleton<ITokenConfiguration, TokenConfiguration>();
            
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Car>, CarValidator>();
            services.AddTransient<IValidator<UserLoginModel>, UserLoginModelValidator>();

            var tokenConfiguration = new TokenConfiguration(Configuration);
            var authPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidateIssuer = true;
                o.TokenValidationParameters.ValidIssuer = tokenConfiguration.Issuer;
                o.TokenValidationParameters.ValidateIssuerSigningKey = true;
                o.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret));
                o.TokenValidationParameters.ValidateAudience = false;
                o.TokenValidationParameters.ValidateLifetime = true;
            });

            services.AddAuthorization(auth => auth.AddPolicy("Baerer", authPolicy));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoShopAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
