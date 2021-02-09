using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollention)  
        {
            serviceCollention.AddDbContext<ProgrammersBlogContext>();
            serviceCollention.AddIdentity<User, Role>(options=> {

                //user password options
                options.Password.RequireDigit = false; //rakam bulunmasının zorunlu olması için true olmalı
                options.Password.RequiredLength = 5; 
                options.Password.RequiredUniqueChars = 0; //eklemesi gereken özel karakterler sayısı
                options.Password.RequireNonAlphanumeric = false; //dolar işaret, g,b, özel karakterlerin gerekli olup olmaması
                options.Password.RequireLowercase = false; //karakterlerin küçük içerip içermemesine bakıyor
                options.Password.RequireUppercase = false; //büyük harf zorunluluğu
                //user username and email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; //aynı adresle ikinci kayıt olamaz

            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            serviceCollention.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollention.AddScoped<ICategoryService, CategoryManager>();
            serviceCollention.AddScoped<IArticleService, ArticleManager>();
            return serviceCollention;
        }
    }
}
