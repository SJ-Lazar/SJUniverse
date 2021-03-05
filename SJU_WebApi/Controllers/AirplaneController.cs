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
    public class AirplaneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AirplaneController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Airplane
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirplaneModel>>> GetAirplanes()
        {
            return await _context.Airplanes.ToListAsync();
        }

        // GET: api/Airplane/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirplaneModel>> GetAirplaneModel(int id)
        {
            var airplaneModel = await _context.Airplanes.FindAsync(id);

            if (airplaneModel == null)
            {
                return NotFound();
            }

            return airplaneModel;
        }

        // PUT: api/Airplane/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirplaneModel(int id, AirplaneModel airplaneModel)
        {
            if (id != airplaneModel.AirplaneId)
            {
                return BadRequest();
            }

            _context.Entry(airplaneModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirplaneModelExists(id))
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

        // POST: api/Airplane
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirplaneModel>> PostAirplaneModel(AirplaneModel airplaneModel)
        {
            _context.Airplanes.Add(airplaneModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirplaneModel", new { id = airplaneModel.AirplaneId }, airplaneModel);
        }

        // DELETE: api/Airplane/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirplaneModel(int id)
        {
            var airplaneModel = await _context.Airplanes.FindAsync(id);
            if (airplaneModel == null)
            {
                return NotFound();
            }

            _context.Airplanes.Remove(airplaneModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirplaneModelExists(int id)
        {
            return _context.Airplanes.Any(e => e.AirplaneId == id);
        }
    }
}
