using Animes.IOC;
using CDL.Integration.Workers.Factories;
using CDL.Integration.Workers.Workers.Animes;
using CDL_Integration.Workers.Extensions;
using CDL_Integration.Workers.Logs;
using CrystalQuartz.AspNetCore;
using LIb.Discord;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using Serilog;

namespace CDL.Integration.Workers
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJobs();

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddScoped<IWebDriver>(x => new ChromeDriver(@"D:\Freelances\Bot\anime-bot\Drivers\"));
            services.InjectBot();
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton<IJobListener, LogsJobListener>();
            services.AddSingleton<IScheduler>(provider =>
            {
                StdSchedulerFactory schedulerFactory = new();
                

                IScheduler scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.ListenerManager.AddJobListener(provider.GetService<IJobListener>(), GroupMatcher<JobKey>.AnyGroup());

                scheduler.Configure<SaikoAnimeJob>(group: "Caxias do sul",
                                                          description: "Atulização de dados de companias e seus usuarios",
                                                          cron: "0 0 1 ? * * *");
                scheduler.Start();
                return scheduler;
            });

            services.AddInternalDependencies();
        }

        public void Configure(IApplicationBuilder app, IScheduler scheduler, ILoggerFactory loggerFactory)
        {
            string basePath = configuration.GetSection("Aplicacao:Path").Value;

            if (!string.IsNullOrEmpty(basePath))
            {
                if (!basePath.StartsWith('/'))
                    basePath = "/" + basePath;

                app.UsePathBase(basePath);

                app.Use(async (context, next) =>
                {
                    context.Request.PathBase = basePath;
                    await next.Invoke();
                });
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            loggerFactory.AddSerilog();

            app.UseCrystalQuartz(() => scheduler);

            RewriteOptions option = new RewriteOptions();
            option.AddRedirect("^$", "quartz");

            app.UseRewriter(option);
        }
    }
}
