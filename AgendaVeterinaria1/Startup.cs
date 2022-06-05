using AgendaVeterinaria1.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaVeterinaria1
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddDbContext<AgendaDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			
	//	app.Run(async(context))=>
	//{
	//			await context.Response.WriteAsync("HelloWorld");
	//		});

		}
	}
}

