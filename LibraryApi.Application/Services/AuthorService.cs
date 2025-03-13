using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;

namespace LibraryApi.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public  async Task AddAuthorAsync(Author author) => await _authorRepository.AddAsync(author);

        public async Task DeleteAuthorAsync(Guid id) => await _authorRepository.DeleteAsync(id);

        public async Task<Author> GetAuthorByIdAsync(Guid id) => await _authorRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Author>> GetAuthorsAsync() => await _authorRepository.GetAllAsync();

        public async Task UpdateAuthorAsync(Author author) => await _authorRepository.UpdateAsync(author);
    }


}
