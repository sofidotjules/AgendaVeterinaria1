using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaVeterinaria1.Context;
using AgendaVeterinaria1.Models;

namespace AgendaVeterinaria1.Controllers
{
    public class ProfesionalsController : Controller
    {
        private readonly AgendaDBContext _context;

        public ProfesionalsController(AgendaDBContext context)
        {
            _context = context;
        }

        // GET: Profesionals
        public async Task<IActionResult> Index()
        {
              return _context.Profesionales != null ? 
                          View(await _context.Profesionales.ToListAsync()) :
                          Problem("Entity set 'AgendaDBContext.Profesionales'  is null.");
        }

        // GET: Profesionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profesionales == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .FirstOrDefaultAsync(m => m.IDProfesional == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesionals/Create
        public IActionResult Create()
        {
            ViewBag.Especialidades = _context.Especialidades.ToList();
            return View();
        }

        // POST: Profesionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDProfesional,Matricula,Nombre,DNI,TipoProfesional,Email,Contrasenia")] Profesional profesional, IFormCollection formCollection)
        {
            var especialidades = formCollection["Especialidades"];
            ModelState.Remove("Especialidades");

            if (ModelState.IsValid)
            {
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesional);

        }

        // GET: Profesionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Especialidades = _context.Especialidades.ToList();
            if (id == null || _context.Profesionales == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }
            return View(profesional);
        }

        // POST: Profesionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDProfesional,Matricula,Nombre,DNI,TipoProfesional,Email")] Profesional profesional, IFormCollection formCollection)
        {
            if (id != profesional.IDProfesional)
            {
                return NotFound();
            }

            var especialidades = formCollection["Especialidades"];
            ModelState.Remove("Especialidades");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionalExists(profesional.IDProfesional))
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
            return View(profesional);
        }

        // GET: Profesionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profesionales == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .FirstOrDefaultAsync(m => m.IDProfesional == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profesionales == null)
            {
                return Problem("Entity set 'AgendaDBContext.Profesionales'  is null.");
            }
            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional != null)
            {
                _context.Profesionales.Remove(profesional);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionalExists(int id)
        {
          return (_context.Profesionales?.Any(e => e.IDProfesional == id)).GetValueOrDefault();
        }

        public IActionResult GoToAgenda(int id) 
        {

            return RedirectToAction("Create", "Agenda");
        }
    }
}
