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
      
        public void ConfigureServices(IServiceCollection services)  //dbcontenxt services katmanýnda extension klasöründe tanýmlandý.
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//frontend d her bir iþlemi derlemeden kaydederek görebilmeyi saðlar
            services.AddAutoMapper(typeof(Startup));  //derlenme esnasýnda mapping sýnýflarýný bulup ekliyor
            services.LoadMyServices();
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); //sitede bulunmayan sayfaya gidilirse 404 döndürür. bu yazýlmazsa beyaz sayfa döndürür. kolaylýk için eklendi
            }
            app.UseStaticFiles(); //resimler cssler js dosyalarý için

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
