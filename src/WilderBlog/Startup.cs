﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using WilderBlog.Data;
using WilderBlog.Logger;
using WilderBlog.MetaWeblog;
using WilderBlog.Services;
using WilderBlog.Services.DataProviders;
using WilderMinds.MetaWeblog;

namespace WilderBlog
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration config, IHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection svcs)
        {
            svcs.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            if (_env.IsDevelopment() && _config.GetValue<bool>("MailService:TestInDev") == false)
            {
                svcs.AddTransient<IMailService, LoggingMailService>();
            }
            else
            {
                svcs.AddTransient<IMailService, MailService>();
            }
            svcs.AddTransient<GoogleCaptchaService>();

            svcs.AddDbContext<WilderContext>(ServiceLifetime.Scoped);

            svcs.AddIdentity<WilderUser, IdentityRole>()
              .AddEntityFrameworkStores<WilderContext>();

            if (_config["WilderDb:TestData"] == "True")
            {
                svcs.AddScoped<IWilderRepository, MemoryRepository>();
            }
            else
            {
                svcs.AddScoped<IWilderRepository, WilderRepository>();
            }

            // Localization part
            //svcs.AddLocalization(options => options.ResourcesPath = "Resources");
            svcs.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            svcs.AddTransient<WilderInitializer>();
            svcs.AddScoped<AdService>();

            // Data Providers (non-EF)
            svcs.AddScoped<CalendarProvider>();
            svcs.AddScoped<CoursesProvider>();
            svcs.AddScoped<PublicationsProvider>();
            svcs.AddScoped<TalksProvider>();
            svcs.AddScoped<VideosProvider>();
            svcs.AddScoped<JobsProvider>();
            svcs.AddScoped<CertsProvider>();
            svcs.AddScoped<TestimonialsProvider>();
            svcs.AddTransient<IImageStorageService, ImageStorageService>();

            // Supporting Live Writer (MetaWeblogAPI)
            svcs.AddMetaWeblog<WilderWeblogProvider>();

            // Add Caching Support
            svcs.AddMemoryCache(opt => opt.ExpirationScanFrequency = TimeSpan.FromMinutes(5));

            // Add MVC to the container
            svcs.AddControllersWithViews();

            svcs.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            svcs.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              ILoggerFactory loggerFactory,
                              IMailService mailService,
                              IServiceScopeFactory scopeFactory)
        {
            // Add the following to the request pipeline only in development environment.
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Early so we can catch the StatusCode error
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Exception");

                // Support logging to email
                loggerFactory.AddEmail(mailService, LogLevel.Critical);

                app.UseHttpsRedirection();
            }

            //var supportedCultures = new[] { "en-US", "de-DE" };
            //var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            //    .AddSupportedCultures(supportedCultures)
            //    .AddSupportedUICultures(supportedCultures);
            //app.UseRequestLocalization(localizationOptions);            

            // Support MetaWeblog API
            app.UseMetaWeblog("/livewriter");

            // Rewrite old URLs to new URLs
            app.UseUrlRewriter();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Email Uncaught Exceptions
            if (_config["Exceptions:TestEmailExceptions"].ToLower() == "true" || !_env.IsDevelopment())
            {
                app.UseMiddleware<EmailExceptionMiddleware>();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("de-DE"),
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            requestLocalizationOptions.RequestCultureProviders.Insert(0, requestProvider);

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute(name: "default", pattern: "{controller=Root}/{action=Index}/{id?}");
            });
        }

    }
}
