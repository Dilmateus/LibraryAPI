using LibraryApi.Domain;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync(
        Func<IQueryable<Author>, IQueryable<Author>> include = null)
            {
                IQueryable<Author> query = _context.Authors;

                if (include != null)
                {
                    query = include(query);
                }

                return await query.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id) => await _context.Authors.FindAsync(id);
        public async Task AddAsync(Author entity) { _context.Authors.Add(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Author entity) { _context.Authors.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id)
        {
            var author = await GetByIdAsync(id);
            if (author != null) { _context.Authors.Remove(author); await _context.SaveChangesAsync(); }
        }

        public Task<IEnumerable<TResult>> GetSelectedColumnsAsync<TResult>(System.Linq.Expressions.Expression<Func<Author, TResult>> selector)
        {
            throw new NotImplementedException();
        }
    }
}
