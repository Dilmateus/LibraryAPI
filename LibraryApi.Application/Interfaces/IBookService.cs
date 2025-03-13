

using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<BookDto>> GetBookForSelectAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task AddBookAsync(Book user);
        Task UpdateBookAsync(Book user);
        Task DeleteBookAsync(Guid id);
    }
}
