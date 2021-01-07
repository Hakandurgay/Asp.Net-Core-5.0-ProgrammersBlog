using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Abstract;
using ProgrammersBlog.Shared.Entities.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository    //sadece Iarticlerepository kalırsa içi boş soyut fonksiyonlar gelir. somutlarını efentity içine yazmıştık. onu da dahil edersek içi doluları kullanabilirz
    {                                                                                     //efentityde hepsinde ortak olanlar var. eğer farklı yazmak isteniyorsa Iarticlerepositorye yazılır
        public EfArticleRepository(DbContext context) : base(context)
        {

        }
    }
}
