using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple_User_Management_API.Interfaces;
using Simple_User_Management_API.Middleware;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Services;
using Simple_User_Management_API.UnitOfWork.Interface;
using Simple_User_Management_API.UnitOfWork.Service;
using System;

namespace Simple_User_Management_API
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
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddTokenAuthentication(Configuration);
            services.AddDbContext<UserManagementContext>(options => options.UseSqlServer(Configuration["SqlServerConnectionString"]));
            services.AddScoped<IUnitOfWork, UnitOfWork.Service.UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<JWTConfig>(new JWTConfig
            {
                Secret = Configuration.GetValue("JwtConfig", "secret"),
                ExpirationInMinutes = Configuration.GetValue("JwtConfig", "expirationInMinutes")
            });
            services.AddSingleton<IAuthService, JWTService>();
            services.AddSingleton<EmailConfig>(new EmailConfig 
            {
                Email = Configuration.GetValue("EmailConfig","Email"),
                Password = Configuration.GetValue("EmailConfig", "Password")
            });
            services.AddSingleton<IEmailService,EmailService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}