

using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using LibraryApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(
         Func<IQueryable<Book>, IQueryable<Book>> include = null)
            {
            IQueryable<Book> query = _context.Books;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }


        public async Task<Book> GetByIdAsync(Guid id) => await _context.Books.FindAsync(id);
        public async Task AddAsync(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book entity) { _context.Books.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id)
        {
            var author = await GetByIdAsync(id);
            if (author != null) { _context.Books.Remove(author); await _context.SaveChangesAsync(); }
        }
        public Task<IEnumerable<TResult>> GetSelectedColumnsAsync<TResult>(System.Linq.Expressions.Expression<Func<Book, TResult>> selector)
        {
            throw new NotImplementedException();
        }
    }

}
