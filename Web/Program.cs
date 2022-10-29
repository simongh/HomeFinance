using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HomeFinance
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(Constants).Assembly);
			builder.Services.AddAutoMapper(typeof(Constants).Assembly);
			builder.Services.AddServices();
			builder.Services.AddValidators();
			builder.Services.AddDbContext(options =>
			{
				options.UseSqlite(builder.Configuration.GetConnectionString("homeFinance"));
			});

			var app = builder.Build();

			ApplyMigrations(app);

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
			}

			app
				.UseStaticFiles()
				.UseRouting();
			app
				.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}

		private static void ApplyMigrations(IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();

			scope.ServiceProvider.Migrate();
		}
	}
}