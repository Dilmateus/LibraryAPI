using LibraryApi.Domain;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApi.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(
        Func<IQueryable<User>, IQueryable<User>> include = null)
        {
            IQueryable<User> query = _context.Users;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);

        public async Task AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TResult>> GetSelectedColumnsAsync<TResult>(Expression<Func<User, TResult>> selector)
        {
            return await _context.Users.Select(selector).ToListAsync();
        }
    }
}
