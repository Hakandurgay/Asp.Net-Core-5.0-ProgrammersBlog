using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Entities.Concrete
{
    public class Role :IdentityRole<int> // EntityBase, IEntity
    {



        //public string Name { get; set; }
        //public string Description { get; set; }
        //public ICollection<User> Users { get; set; }
    }
}
