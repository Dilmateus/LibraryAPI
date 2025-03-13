using AutoMapper;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.API.Controllers
{
  
    [Route("api/users")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => Ok(await _userService.GetUsersAsync());
        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersSelect() => Ok(await _userService.GetUserForSelectAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<User>> GetUser(Guid id) => Ok(await _userService.GetUserByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id) return BadRequest();
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
