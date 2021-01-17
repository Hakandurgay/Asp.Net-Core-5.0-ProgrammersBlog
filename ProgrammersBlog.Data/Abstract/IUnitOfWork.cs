
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable  //IDisposable yerine IAsuncDisposable
    {
        IArticleRepository Articles { get; } //unitofwork.Articles şeklinde erişiilir
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        //IRoleRepository Roles { get; }
        //IUserRepository Users { get; }   //idendity ile geleceğinden kendimizini oluşturulduğu sınıflar silindi

        //uniofworks.categories.addaysnv(category)
        //uniofworks.categories.addaysnv(user)
        //uniofworks.categories.saveasync()  şeklinde olur
        Task<int> SaveAsync();
    }
}
