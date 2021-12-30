using System;
using AutoMapper;
using Groot.API.Controllers.Infrastructure;
using Groot.API.Infrastructure;
using Groot.Service.Common;
using Groot.Service.Product;
using Groot.Service.User;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Groot.API
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
            // added configuration to mapper.
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();//creating mapper.
            services.AddSingleton(mapper);//at run one time when project start. injected.
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddControllers();
            services.AddMemoryCache();

            //added redis cache
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = "localhost:6379";
            });
            services.AddScoped<LoginFilter>();
            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage());

            services.AddHangfireServer();
            services.AddTransient(options => {
                return new AppSettings
                {
                    MailServer = $"{Configuration["MailServer"]}",
                    MailPort = Convert.ToInt32(Configuration["MailPort"]),
                    MailPassword = $"{Configuration["MailPassword"]}",
                    MailSendFrom = $"{Configuration["MailSendFrom"]}",
                    MailSendFromName = $"{Configuration["MailSendFromName"]}"
                };
            });
            services.AddSingleton<IWelcomeMailJob, WelcomeMailJob>();
            services.AddSingleton<IMailSender, MailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
