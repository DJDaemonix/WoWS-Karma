using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Wargaming.WebAPI.Models;
using Wargaming.WebAPI.Requests;
using WowsKarma.Api.Data;
using WowsKarma.Api.Services;

namespace WowsKarma.Api
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

			services.AddDbContext<ApiDbContext>(options => 
				options.UseSqlServer(Configuration.GetConnectionString("ApiDbConnectionString"), 
					providerOptions => providerOptions.EnableRetryOnFailure()));

			services.AddHttpClient();

			services.AddSingleton(new WorldOfWarshipsHandlerOptions(GetApiRegion(), Configuration["Api:AppId"]));
			services.AddSingleton<WorldOfWarshipsHandler>();
			services.AddSingleton<VortexApiHandler>();
			services.AddSingleton<WgApiFetcherService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			if (env.IsProduction()) // Nginx configuration step
			{
				app.UseForwardedHeaders(new ForwardedHeadersOptions
				{
					ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
				});
			}

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}

		private Region GetApiRegion() => Configuration["Api:Region"] switch
		{
			"EU" => Region.EU,
			"NA" => Region.NA,
			"CIS" or "RU" => Region.CIS,
			"ASIA" => Region.ASIA,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
