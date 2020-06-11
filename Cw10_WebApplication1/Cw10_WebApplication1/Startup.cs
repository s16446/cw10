using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10_WebApplication1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication1.DAL;
using WebApplication1.Services;

namespace WebApplication1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IConfiguration _configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IStudentDbService, SqlServerStudentDbService>();
			services.AddDbContext<s16446Context>(options =>
				options.UseSqlServer(_configuration.GetConnectionString("s16446Database")));
			
            services.AddScoped<IDbService, MockDbService>(); 

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
		if (env.IsDevelopment())
		{
		app.UseDeveloperExceptionPage();
		}

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
		endpoints.MapControllers();
		});
		}
	}
}
