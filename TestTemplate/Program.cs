using TestTemplate.Respository;
using Microsoft.EntityFrameworkCore;
using TestTemplate.Models;

namespace TestTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //string conStr = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLNongSan_new;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var conStr = builder.Configuration.GetConnectionString("QlnongSanNewContext");
            builder.Services.AddDbContext<QlnongSanNewContext>(x => x.UseSqlServer(conStr));

            builder.Services.AddScoped<ILoaiNongSan, LoaiNongSanRepository>();

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseSession(); // them session

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}