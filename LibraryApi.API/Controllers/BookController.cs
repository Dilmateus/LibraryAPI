using AutoMapper;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/books")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId  // Certifique-se de que AuthorId está sendo atribuído
        };

        await _bookService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();
        return Ok(_mapper.Map<BookDto>(book));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetBooksAsync();

        var result = books.Select(b => new {
            b.Id,
            b.Title,
            AuthorName = b.Author?.Name ?? "Autor não encontrado" // Verificação nula
        });

        return Ok(result);
    }

}
