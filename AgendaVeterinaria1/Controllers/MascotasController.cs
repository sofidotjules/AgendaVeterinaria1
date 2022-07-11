using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaVeterinaria1.Context;
using AgendaVeterinaria1.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgendaVeterinaria1.Controllers
{
   
    public class MascotasController : Controller
    {
        private readonly AgendaDBContext _context;

        public MascotasController(AgendaDBContext context)
        {
            _context = context;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index(int id=0)
        {
            if (id== 0)
            {
                return _context.Mascotas != null ?
                         View( await _context.Mascotas.ToListAsync()) :
                         Problem("Entity set 'AgendaDBContext.Mascotas'  is null.");
            }
            var cliente = _context.Clientes.Include(x => x.Mascotas).Where(x => x.IDCliente == id).FirstOrDefault();
            ViewBag.IDCliente = id;
            return cliente != null && cliente.Mascotas.Count != 0? 
                          View(cliente.Mascotas) :
                          Problem("Entity set 'AgendaDBContext.Mascotas'  is null.");
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .FirstOrDefaultAsync(m => m.IDMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create(int id=0)
        {
            ViewBag.IDCliente= id;
            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDMascota,Nombre,TipoMascota,Detalle")] Mascota mascota, IFormCollection formCollection)
        {
            var idCliente = formCollection["IdCliente"];
          //  ViewBag.IDCliente = Convert.ToInt32(idCliente);
            if (ModelState.IsValid)
            {
                var cliente = _context.Clientes.Include(x => x.Mascotas).Where(x => x.IDCliente == Convert.ToInt32(idCliente)).FirstOrDefault();
                cliente.Mascotas.Add(mascota);
                _context.Add(mascota);
                _context.SaveChanges();
                return RedirectToAction("Index", "Mascotas", new { id= Convert.ToInt32(idCliente) });
            }
            
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IDMascota,Nombre,TipoMascota,Detalle")] Mascota mascota)
        {
            if (id != mascota.IDMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascota);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.IDMascota))
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
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .FirstOrDefaultAsync(m => m.IDMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mascotas == null)
            {
                return Problem("Entity set 'AgendaDBContext.Mascotas'  is null.");
            }
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
          return (_context.Mascotas?.Any(e => e.IDMascota == id)).GetValueOrDefault();
        }
    }
}
