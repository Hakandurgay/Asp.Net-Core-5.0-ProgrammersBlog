using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Mvc.AutoMapper.Profiles;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
      
        public void ConfigureServices(IServiceCollection services)  //dbcontenxt services katmanýnda extension klasöründe tanýmlandý.
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt=> {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            } ) ;//frontend d her bir iþlemi derlemeden kaydederek görebilmeyi saðlar
                 //    services.AddAutoMapper(typeof(Startup));  //derlenme esnasýnda mapping sýnýflarýný bulup ekliyor
            services.AddSession();
           services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile), typeof(UserProfile)); 
            services.LoadMyServices();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //sadece http olan baðlantýlar kabul ediliyro
                    SameSite = SameSiteMode.Strict, //saldýrganlar kendi cookiemizi farklý adresler kullanarak bizmiþiz gibi kullanabilir. strict farklý adreslerden gelen istekleri engeller
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, //burasýnýn gerçek projelerde always olmasý gerek, test olduðu için böyle ayarlandýr. bilgiler http üzerinden aktarýlmasýný saðlar
                };
                options.SlidingExpiration = true; //giriþ yapma süresini belirler. giriþ yaptýktan sonra kullanýcýya tanýnan zaman. bu süre içerisinde kullanýcýnýn ayný cookie bilgieri üzerinden tekrar giriþ yapmasýný engeller
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); //7 gün
                options.AccessDeniedPath = new PathString("/Admin/User/Logout");
            });
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitede bulunmayan sayfaya gidilirse 404 döndürür. bu yazýlmazsa beyaz sayfa döndürür. kolaylýk için eklendi
            }
            app.UseSession();
            app.UseStaticFiles(); //resimler cssler js dosyalarý için

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
