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
            // Cấu hình dịch vụ cho Session và Cookie
            builder.Services.AddDistributedMemoryCache(); // Cung cấp bộ nhớ lưu trữ cho Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
                options.Cookie.HttpOnly = true; // Giới hạn truy cập cookie từ client-side (JavaScript)
                options.Cookie.IsEssential = true; // Cần thiết cho việc lưu session ngay cả khi người dùng không chấp nhận cookie
            });
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
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseSession(); // them session

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");
            

            app.Run();
        }
    }
}