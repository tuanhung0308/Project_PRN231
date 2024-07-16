using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Project_PRN231_API.Models;
using Project_PRN231_API.ViewModel.User;

namespace Project_PRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FarmManagement_PRN231Context _context;
        private readonly IMapper _mapper;

        public UserController(FarmManagement_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Include(u => u.Role)
                .ToList();

            var userVMs = _mapper.Map<List<UserVM>>(users);

            return Ok(userVMs);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var userVM = _mapper.Map<UserVM>(user);

            return Ok(userVM);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult PostUser(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userVM);

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userVM);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, UserVM userVM)
        {
            if (id != userVM.UserId)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userVM);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        [HttpPost("login")]
        [EnableQuery]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == user.Username);

            if (existingUser != null && existingUser.PasswordHash == user.PasswordHash)
            {
                return Ok(existingUser);
            }
            return Unauthorized("Invalid email or password or you banned!");
        }
    }
}
