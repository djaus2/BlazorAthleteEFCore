using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAthleteEFCore.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private readonly SqlDbContext _dbContext;

        public AthletesController(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<Athlete>> Get()
        {
            return await _dbContext.Athletes.ToListAsync();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create([FromBody]Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                athlete.Id = Guid.NewGuid().ToString();
                _dbContext.Add(athlete);
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Athlete> Details(string id)
        {
            return await _dbContext.Athletes.FindAsync(id);
        }

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<bool> Edit(string id, [FromBody]Athlete athlete)
        {
            if (id != athlete.Id)
            {
                return false;
            }

            _dbContext.Entry(athlete).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<bool> DeleteConfirmed(string id)
        {
            var athlete = await _dbContext.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return false;
            }

            _dbContext.Athletes.Remove(athlete);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
