using AutoMapper;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/authors")]
[ApiController]
[Authorize]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorService.AddAuthorAsync(author);
            var createdAuthorDto = _mapper.Map<AuthorDto>(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, createdAuthorDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor", detalhes = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        try
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound(new { message = "Autor não encontrado!" });

            return Ok(_mapper.Map<AuthorDto>(author));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor", detalhes = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        try
        {
            var authors = await _authorService.GetAuthorsAsync();
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor", detalhes = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        try
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound(new { message = "Autor não encontrado!" });

            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor", detalhes = ex.Message });
        }
    }
}
