using AutoMapper;
using DAL;
using DAL.Model;
using DAL.Service;
using DAL.Services.ComentsService;
using DAL.UnitWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RssCrawleraApi.Email;
using RssCrawleraApi.EmailService;
using RssCrawleraApi.SignalR;
using System;

namespace RssCrawleraApi
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			Configuration = InitAppSettings(env);
		}

		public IConfiguration Configuration { get; }

		protected virtual IConfigurationRoot InitAppSettings(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			return builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<RssHub>();
            services.AddTransient<ModelContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMapper>(i => new Mapper(MapConfig()));
            services.AddTransient<IEmailSender, Emailer>();
            services.AddTransient<IComentsService, ComentsService>();
            services.Configure<SmtpConfig>(Configuration.GetSection("SmtpConfig"));
            services.AddCors();
			services.AddSignalR();
			services.AddMvc(i => {
                i.RespectBrowserAcceptHeader = true;
                i.InputFormatters.Add(new XmlSerializerInputFormatter());
                i.OutputFormatters.Add(new XmlSerializerOutputFormatter());
             
            });
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
				builder.WithOrigins("http://localhost:4200")
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseSignalR(routes =>
			{
				routes.MapHub<RssHub>("RssHub");
			});

            app.UseExceptionHandler();
			app.UseMvc();
		}

        private MapperConfiguration MapConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDbo>()
                .ForMember("FirstName", x => x.MapFrom(c => c.FirstName))
                .ForMember("SecondName", x => x.MapFrom(c => c.SecondName))
                .ForMember("Email", x => x.MapFrom(c => c.Email))
                .ForMember("DateOfRegistration", x => x.MapFrom(c => DateTime.UtcNow))
                ;
            });
        }
    }
}
