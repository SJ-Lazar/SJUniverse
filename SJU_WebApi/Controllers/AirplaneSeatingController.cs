using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SJU_DataModel.Models;
using SJU_EFCore;

namespace SJU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneSeatingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AirplaneSeatingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AirplaneSeating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirplaneSeatingModel>>> GetAirplaneSeatings()
        {
            return await _context.AirplaneSeatings.ToListAsync();
        }

        // GET: api/AirplaneSeating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirplaneSeatingModel>> GetAirplaneSeatingModel(int id)
        {
            var airplaneSeatingModel = await _context.AirplaneSeatings.FindAsync(id);

            if (airplaneSeatingModel == null)
            {
                return NotFound();
            }

            return airplaneSeatingModel;
        }

        // PUT: api/AirplaneSeating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirplaneSeatingModel(int id, AirplaneSeatingModel airplaneSeatingModel)
        {
            if (id != airplaneSeatingModel.AirplaneSeatingId)
            {
                return BadRequest();
            }

            _context.Entry(airplaneSeatingModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirplaneSeatingModelExists(id))
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

        // POST: api/AirplaneSeating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirplaneSeatingModel>> PostAirplaneSeatingModel(AirplaneSeatingModel airplaneSeatingModel)
        {
            _context.AirplaneSeatings.Add(airplaneSeatingModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirplaneSeatingModel", new { id = airplaneSeatingModel.AirplaneSeatingId }, airplaneSeatingModel);
        }

        // DELETE: api/AirplaneSeating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirplaneSeatingModel(int id)
        {
            var airplaneSeatingModel = await _context.AirplaneSeatings.FindAsync(id);
            if (airplaneSeatingModel == null)
            {
                return NotFound();
            }

            _context.AirplaneSeatings.Remove(airplaneSeatingModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirplaneSeatingModelExists(int id)
        {
            return _context.AirplaneSeatings.Any(e => e.AirplaneSeatingId == id);
        }
    }
}
