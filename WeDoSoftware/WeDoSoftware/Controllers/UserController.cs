using Microsoft.AspNetCore.Mvc;
using WeDoSoftware.Application.DTOs.UserDTOs;
using WeDoSoftware.Application.ServiceInterfaces;

namespace WeDoSoftware.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {    
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var userId = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = userId }, dto);
        }
        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto);
            return NoContent();
        }
        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
