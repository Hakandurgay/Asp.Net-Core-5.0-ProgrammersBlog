using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>(); //user add dto yu user sınıfına map eder
            CreateMap<User, UserUpdateDto>(); 
            CreateMap<UserUpdateDto, User>(); 
        }
    }
}
