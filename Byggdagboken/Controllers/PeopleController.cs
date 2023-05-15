using Byggdagboken.Data;
using Byggdagboken.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Byggdagboken.Controllers
{
    public class PeopleController : Controller
    {
        readonly PeopleDBContext _context;

        public PeopleController(PeopleDBContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            return _context.Persons != null ?
                        View(await _context.Persons.ToListAsync()) :
                        Problem("Entity set 'PeopleDBContext.Persons'  is null.");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country")] Person person)
        {
            if (ModelState.IsValid)
            {
                var input = new Person()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = person.Name,
                    Country = person.Country,
                };

                _context.Add(input);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Country")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'PeopleDBContext.Persons'  is null.");
            }

            var person = await _context.Persons.FindAsync(id);

            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        bool PersonExists(string id) => (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}