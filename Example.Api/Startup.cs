using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Extensions.Converters;
using MVC.Extensions.Filters;
using MVC.Extensions.JWT;

namespace Example.Api
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
			services.AddMvc(options =>
			{
				options.Filters.Add<HttpGlobalExceptionFilter>();
				options.Filters.Add<ValidateModelStateFilter>();
			}).AddJsonOptions(options =>
			{
				options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
				options.SerializerSettings.Converters.Add(new LongToStringConverter());
			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddJwtBearer("99a62aa2-df24-4a7a-b418-68aa4f332369");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
