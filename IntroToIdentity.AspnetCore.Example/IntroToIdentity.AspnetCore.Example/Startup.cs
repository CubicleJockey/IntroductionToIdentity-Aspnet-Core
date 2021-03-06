﻿using System;
using System.Security;
using IntroToIdentity.AspnetCore.Example.Configurations;
using IntroToIdentity.AspnetCore.Example.Constants;
using IntroToIdentity.AspnetCore.Example.Data;
using IntroToIdentity.AspnetCore.Example.Models;
using IntroToIdentity.AspnetCore.Example.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntroToIdentity.AspnetCore.Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <exception cref="SecurityException">The caller does not have the required permission to perform this operation.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="variable">variable</paramref> is null.</exception>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (EnvironmentVariables.GetDatabase == AppSettingsConstants.SqlServer)
                {
                    options.UseSqlServer(Configuration.GetConnectionString(AppSettingsConstants.DefaultSqlServerConnection));
                }
                else
                {
                    options.UseNpgsql(Configuration.GetConnectionString(AppSettingsConstants.DefaultPostgressConnection));
                }
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            ConfigureIdentityOptions(services);
            ConfigureApplicationCookies(services);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //Active Middleware for Authentication
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region Helper Methods

        private static void ConfigureIdentityOptions(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                #region Password Settings

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                #endregion Password Settings

                #region Lockout settings

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                #endregion Lockout settings

                #region  User settings

                options.User.RequireUniqueEmail = true;

                #endregion  User settings

                #region Signin Settings

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                #endregion Signin Settings
            });
        }

        private static void ConfigureApplicationCookies(IServiceCollection services)
        {
            const string CONTROLLER = "Account";
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "TutorialCookieOfAwesome";
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = $"/{CONTROLLER}/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = $"/{CONTROLLER}/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = $"/{CONTROLLER}/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });
        }

        #endregion Helper Methods
    }
}
