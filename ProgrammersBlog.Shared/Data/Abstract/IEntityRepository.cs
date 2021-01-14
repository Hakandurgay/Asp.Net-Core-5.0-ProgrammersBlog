using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Data.Abstract
{

    //tüm entityler için dal classlarında kullanılabilecek metodlar
    public interface IEntityRepository<T> where T : class, IEntity, new()  // T nin hangileri olabileceğini söylüyor
    {
        Task<T> GetAsync(Expression<Func<T, bool>> Predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> Predicate = null, params Expression<Func<T, object>>[] includeProperties); // null gelirse her şeyi listeler, null gelmezse verilen filtreye göre listeleme yapar
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); //kayıdın önceden var mı yok mu bakılması için
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);  //kayıtların sayısını vs bulmak için 


        //exporessionlar var kullanici=repository.GetAsync(k=>k.Id==15) şeklinde lamda exporessionların kullanılmasını sağlar
        //predicate neye göre istedğimiz örneğin yukarıdaki örnekte id 
        //kullanıcının yanında yorumlarını da getirilimesi isteniyorsa bir expression daha eklenir
        //birden fazla fazla includeProperties verilebileceği için params eklenir. 

    }
}
