using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete
{
    public class EfUserRepository: EfEntityRepositoryBase<User>, IUserRepository
    {
        public EfUserRepository(DbContext context):base(context)
        {

        }
    }
}
