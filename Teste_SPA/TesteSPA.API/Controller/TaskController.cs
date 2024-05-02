using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteSPA.API.Data;
using TesteSPA.API.DTO;
using TesteSPA.API.Model;

namespace TesteSPA.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Tasks!.ToList());
        }

[HttpPost]
public IActionResult Post([FromBody] TaskDTO taskDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var task = taskDTO.toTaskModel();
    var result = _context.Tasks!.Add(task);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetById), new { id = result.Entity.Id }, result.Entity);
}

[HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            var task = _context.Tasks!.FirstOrDefault(x => x.Id == Id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] TaskDTO task)
{
    if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTask = _context.Tasks!.FirstOrDefault(x => x.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.dateTime = task.dateTime;
            existingTask.time = TimeOnly.Parse(task.time);

            _context.Entry(existingTask).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
}
[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks!.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks!.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
}