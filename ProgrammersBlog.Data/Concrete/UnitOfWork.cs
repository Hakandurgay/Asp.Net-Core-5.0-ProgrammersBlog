using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgrammersBlogContext _context;
        private EfArticleRepository _efArticleRepository;
        private EfCategoryRepository _efCategoryRepository;
        private EfCommentRepository _efCommentRepository;
        private EfRoleRepository _efRoleRepository;
        private EfUserRepository _efUserRepository;


        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }

        public IArticleRepository Articles => _efArticleRepository ?? new EfArticleRepository(_context);  //null ilse new ile oluşturur null dğeil ise öncekini döner

        public ICategoryRepository Categories => _efCategoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _efCommentRepository ?? new EfCommentRepository(_context);

        public IRoleRepository Roles => _efRoleRepository ?? new EfRoleRepository(_context);

        public IUserRepository Users => _efUserRepository ?? new EfUserRepository(_context);

    
        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync(); // ?
        }

        public async Task<int> SaveAsync() //save changes metodu gibi
        {
            return await _context.SaveChangesAsync();
        }
    }
}
