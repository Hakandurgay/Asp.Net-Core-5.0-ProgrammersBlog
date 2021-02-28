﻿using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class UserWithRoleViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
