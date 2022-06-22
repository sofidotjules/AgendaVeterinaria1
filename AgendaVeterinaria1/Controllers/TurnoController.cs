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
    public class TurnoController : Controller
    {
        private readonly AgendaDBContext _context;

        public TurnoController(AgendaDBContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            var agendaDBContext = _context.Turnos.Include(t => t.Mascota).Include(t => t.Profesional);
            return View(await agendaDBContext.ToListAsync());
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Mascota)
                .Include(t => t.Profesional)
                .FirstOrDefaultAsync(m => m.IDTurno == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turno/Create
        public IActionResult Create()
        {
            ViewData["IDCliente"] = new SelectList(_context.Clientes, "IDCliente", "IDCliente");
            ViewData["IDProfesional"] = new SelectList(_context.Profesionales, "IDProfesional", "IDProfesional");
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDTurno,TipoDeTurno,Detalle,Fecha,Horario,IDCliente,IDProfesional")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDCliente"] = new SelectList(_context.Clientes, "IDCliente", "IDCliente", turno.IDMascota);
            ViewData["IDProfesional"] = new SelectList(_context.Profesionales, "IDProfesional", "IDProfesional", turno.IDProfesional);
            return View(turno);
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["IDCliente"] = new SelectList(_context.Clientes, "IDCliente", "IDCliente", turno.IDMascota);
            ViewData["IDProfesional"] = new SelectList(_context.Profesionales, "IDProfesional", "IDProfesional", turno.IDProfesional);
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDTurno,TipoDeTurno,Detalle,Fecha,Horario,IDCliente,IDProfesional")] Turno turno)
        {
            if (id != turno.IDTurno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.IDTurno))
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
            ViewData["IDCliente"] = new SelectList(_context.Clientes, "IDCliente", "IDCliente", turno.IDMascota);
            ViewData["IDProfesional"] = new SelectList(_context.Profesionales, "IDProfesional", "IDProfesional", turno.IDProfesional);
            return View(turno);
        }

        // GET: Turno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Mascota)
                .Include(t => t.Profesional)
                .FirstOrDefaultAsync(m => m.IDTurno == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'AgendaDBContext.Turnos'  is null.");
            }
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
          return (_context.Turnos?.Any(e => e.IDTurno == id)).GetValueOrDefault();
        }
    }
}
