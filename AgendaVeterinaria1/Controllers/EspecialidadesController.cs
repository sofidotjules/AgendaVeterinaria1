using AgendaVeterinaria1.Context;
using AgendaVeterinaria1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaVeterinaria1.Controllers
{
    public class EspecialidadesController : Controller
    {
        private readonly AgendaDBContext _context;

        public EspecialidadesController(AgendaDBContext context)
        {
            _context = context;
        }

        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            return _context.Especialidades != null ?
                         View(await _context.Especialidades.ToListAsync()) :
                         Problem("Entity set 'AgendaDBContext.Especialidades'  is null.");
        }

        // GET: HomeController1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.IDEspecialidad == id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDEspecialidad,Descripcion")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidad);
        }


        // GET: HomeController1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDEspecialidad,Descripcion")] Especialidad especialidad)
        {
            if (id != especialidad.IDEspecialidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadExists(especialidad.IDEspecialidad))
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
            return View(especialidad);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.IDEspecialidad == id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return View(especialidad);
        }

        // POST: especialidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialidades == null)
            {
                return Problem("Entity set 'AgendaDBContext.Especialidades'  is null.");
            }
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad != null)
            {
                _context.Especialidades.Remove(especialidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadExists(int id)
        {
            return (_context.Especialidades?.Any(e => e.IDEspecialidad == id)).GetValueOrDefault();
        }
    }
}
