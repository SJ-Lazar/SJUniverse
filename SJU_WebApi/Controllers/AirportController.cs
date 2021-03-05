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
    public class AirportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AirportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Airport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportModel>>> GetAirports()
        {
            return await _context.Airports.ToListAsync();
        }

        // GET: api/Airport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportModel>> GetAirportModel(int id)
        {
            var airportModel = await _context.Airports.FindAsync(id);

            if (airportModel == null)
            {
                return NotFound();
            }

            return airportModel;
        }

        // PUT: api/Airport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirportModel(int id, AirportModel airportModel)
        {
            if (id != airportModel.AirportId)
            {
                return BadRequest();
            }

            _context.Entry(airportModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportModelExists(id))
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

        // POST: api/Airport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirportModel>> PostAirportModel(AirportModel airportModel)
        {
            _context.Airports.Add(airportModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirportModel", new { id = airportModel.AirportId }, airportModel);
        }

        // DELETE: api/Airport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirportModel(int id)
        {
            var airportModel = await _context.Airports.FindAsync(id);
            if (airportModel == null)
            {
                return NotFound();
            }

            _context.Airports.Remove(airportModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportModelExists(int id)
        {
            return _context.Airports.Any(e => e.AirportId == id);
        }
    }
}
