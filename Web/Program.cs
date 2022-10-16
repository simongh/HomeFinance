using MediatR;
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

			var app = builder.Build();

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
	}
}