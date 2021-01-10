using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            //ilki, neyi dönüştüreceği, ikincisi neye dönüştüreceği
            CreateMap<ArticleAddDto, Article>().ForMember(dest=>dest.CreatedDate, op=>op.MapFrom(x=>DateTime.Now)); //article add dto içinde created date yok ama articleda var. dönüştürürken article içinde gördüğü yerde burada tanımladığım değişkeni yazacak
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModifiedDate,op=>op.MapFrom(x=>DateTime.Now));

        }
    }
}
