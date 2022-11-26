using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if(env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseRouting();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapGet("/", async context =>
			{
				await context.Response.WriteAsync("Hello World");
			});
			endpoints.MapControllers();

		});
	}
	public Startup()
	{
	}
}
