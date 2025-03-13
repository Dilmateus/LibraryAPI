using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookServiceRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookServiceRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync() =>
        await _bookServiceRepository.GetAllAsync(
            query => query.Include(b => b.Author)
        );

        public async Task<Book> GetBookByIdAsync(Guid id) => await _bookServiceRepository.GetByIdAsync(id);
        public async Task AddBookAsync(Book book) => await _bookServiceRepository.AddAsync(book);
        public async Task UpdateBookAsync(Book book) => await _bookServiceRepository.UpdateAsync(book);
        public async Task DeleteBookAsync(Guid id) => await _bookServiceRepository.DeleteAsync(id);

        public Task<IEnumerable<BookDto>> GetBookForSelectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
