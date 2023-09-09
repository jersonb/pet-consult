using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConsult.Data;
using PetConsult.Models;
using PetConsult.ViewObjects;
using PetConsult.ViewObjects.Requests;

namespace PetConsult.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly PetConsultContext _context;

        public PetsController(PetConsultContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetResponse>>> GetPet()
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }

            return await _context.Pets.Select(x => new PetResponse
            {
                Id = x.Id,
                Name = x.Name,
                AdoptionDate = x.AdoptionDate,
                Birthday = x.Birthday
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetResponse>> GetPet(int id)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets
                .Select(x => new PetResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    AdoptionDate = x.AdoptionDate,
                    Birthday = x.Birthday
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return pet;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, PetUpdate petUpdate)
        {
            if (id != petUpdate.Id)
            {
                return BadRequest();
            }

            var pet = _context.Pets.FirstOrDefault(p => p.Id == id);

            if (pet == default)
            {
                return BadRequest();
            }

            pet.Name = petUpdate.Name;
            pet.AdoptionDate = petUpdate.AdoptionDate;
            pet.Birthday = petUpdate.Birthday;

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var uri = Url.Action(nameof(GetPet), "pets", new { id }, "https", Request.Host.ToUriComponent())
                ?? throw new InvalidOperationException();

            return Accepted(uri);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(PetCreate petCreate)
        {
            if (_context.Pets == null)
            {
                return Problem("Entity set 'PetConsultContext.Pet'  is null.");
            }

            var pet = new Pet
            {
                Name = petCreate.Name,
                AdoptionDate = petCreate.AdoptionDate,
                Birthday = petCreate.Birthday,
            };

            _context.Pets.Add(pet);

            await _context.SaveChangesAsync();

            var uri = Url.Action(nameof(GetPet), "pets", new { pet.Id }, "https", Request.Host.ToUriComponent())
            ?? throw new InvalidOperationException();

            return Created(uri, new { pet.Name });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            if (_context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeletePet())
        {
            var id =0;
            if (_context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return (_context.Pets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}