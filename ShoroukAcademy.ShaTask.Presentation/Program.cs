using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShoroukAcademy.ShaTask.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace ShoroukAcademy.ShaTask.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();
            var connectionStr = builder.Configuration.GetConnectionString("ShaTask");
            builder.Services.AddDbContext<ShaTaskContext>(
                option =>
                {
                    option.UseSqlServer(connectionStr);
                });
            builder.Services.AddIdentity<IdentityUser,IdentityRole>
           ().AddEntityFrameworkStores<ShaTaskContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/SignIn";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            var app = builder.Build();
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = "/Content",
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Content")
                )
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("route", "{controller=InvoiceDetails}/{action=Index}/{id?}");
            app.Run();
        }
    }
}