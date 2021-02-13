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
      
        public void ConfigureServices(IServiceCollection services)  //dbcontenxt services katman�nda extension klas�r�nde tan�mland�.
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt=> {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            } ) ;//frontend d her bir i�lemi derlemeden kaydederek g�rebilmeyi sa�lar
                 //    services.AddAutoMapper(typeof(Startup));  //derlenme esnas�nda mapping s�n�flar�n� bulup ekliyor
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
                    HttpOnly = true, //sadece http olan ba�lant�lar kabul ediliyro
                    SameSite = SameSiteMode.Strict, //sald�rganlar kendi cookiemizi farkl� adresler kullanarak bizmi�iz gibi kullanabilir. strict farkl� adreslerden gelen istekleri engeller
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, //buras�n�n ger�ek projelerde always olmas� gerek, test oldu�u i�in b�yle ayarland�r. bilgiler http �zerinden aktar�lmas�n� sa�lar
                };
                options.SlidingExpiration = true; //giri� yapma s�resini belirler. giri� yapt�ktan sonra kullan�c�ya tan�nan zaman. bu s�re i�erisinde kullan�c�n�n ayn� cookie bilgieri �zerinden tekrar giri� yapmas�n� engeller
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); //7 g�n
                options.AccessDeniedPath = new PathString("/Admin/User/Logout");
            });
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitede bulunmayan sayfaya gidilirse 404 d�nd�r�r. bu yaz�lmazsa beyaz sayfa d�nd�r�r. kolayl�k i�in eklendi
            }
            app.UseSession();
            app.UseStaticFiles(); //resimler cssler js dosyalar� i�in

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
