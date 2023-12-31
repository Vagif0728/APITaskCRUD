﻿using APITask.DAL;
using APITask.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
       

        public async Task<IActionResult> Get(int page,int take)
        {
          List<Category> categories=  await _context.Categories.Skip((page-1)*take).Take(take).ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Category category=await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);

            if(category==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK,category);
            
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }

        [HttpPut("id")]

        public async Task<IActionResult> Update(int id,string name)
        {

            if(id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Category existed = await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);

            if(existed==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            existed.Name = name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Category existed = _context.Categories.FirstOrDefault(c=>c.Id==id);

            if(existed==null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();



        }
       
    }
}
