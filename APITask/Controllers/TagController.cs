using APITask.DAL;
using APITask.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]


        public async Task<IActionResult> Get(int page, int take)
        {
            List<Tag> tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();

            return Ok(tags);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);

            if (tag == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, tag);

        }

        [HttpPost]

        public async Task<IActionResult> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, tag);
        }

        [HttpPut("id")]

        public async Task<IActionResult> Update(int id, string title)
        {

            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Tag existed = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (existed == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            existed.Title = title;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Tag existed = _context.Tags.FirstOrDefault(t => t.Id == id);

            if (existed == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _context.Tags.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
