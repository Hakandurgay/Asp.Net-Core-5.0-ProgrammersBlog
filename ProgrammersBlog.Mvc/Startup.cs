using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
      
        public void ConfigureServices(IServiceCollection services)  //dbcontenxt services katman�nda extension klas�r�nde tan�mland�.
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//frontend d her bir i�lemi derlemeden kaydederek g�rebilmeyi sa�lar
            services.AddAutoMapper(typeof(Startup));  //derlenme esnas�nda mapping s�n�flar�n� bulup ekliyor
            services.LoadMyServices();
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitede bulunmayan sayfaya gidilirse 404 d�nd�r�r. bu yaz�lmazsa beyaz sayfa d�nd�r�r. kolayl�k i�in eklendi
            }
            app.UseStaticFiles(); //resimler cssler js dosyalar� i�in

            app.UseRouting();

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
