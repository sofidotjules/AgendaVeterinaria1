using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaVeterinaria1.Context;
using AgendaVeterinaria1.Models;
using System.Globalization;

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

        public IActionResult SolicitarTurno()
        {

            ViewData["Mascotas"] = _context.Mascotas;
            ViewData["Especialidades"] = _context.Especialidades;
            // ViewData["IDProfesional"] = new SelectList(_context.Profesionales, "IDProfesional", "IDProfesional");
            return View();
        }

        public JsonResult ObtenerHorasTurno(DateTime fecha, int idEspecialidad, string tipoTurno) 
        {
            try
            {
                int i = 0;
                List<Agenda> agendas = _context.Agendas
                    .Include(x => x.Profesional)
                    .Include(x => x.Profesional.Especialidades)
                    .Where(x => x.Profesional.Especialidades.Select(y => y.IDEspecialidad).Contains(idEspecialidad)
                            && x.Profesional.TipoProfesional.Equals(tipoTurno)
                            && x.FechaDesde <= fecha && x.FechaHasta >= fecha).ToList();
                Agenda agenda = null;
                while (agenda == null && i<agendas.Count())
                {
                    int cantTurnos = _context.Turnos.Where(x => x.IDProfesional == agendas.ElementAt(i).IDProfesional).Count();

                    if (cantTurnos < agendas.ElementAt(i).TopeDeTurnos)
                    {
                        agenda = agendas.ElementAt(i);
                    }
                    i++;
                }
                if (agenda != null)
                {
                    string[] horarios = agenda.FranjaHoraria.Split("-");

                    TimeSpan horaHasta = new TimeSpan((Convert.ToInt32(horarios[1])) / 100, (Convert.ToInt32(horarios[1])) % 100, 0);
                    TimeSpan horaDesde = new TimeSpan((Convert.ToInt32(horarios[0])) / 100, (Convert.ToInt32(horarios[0])) % 100, 0);

                    DateTime TimeOut = DateTime.Today.Add(horaHasta);
                    DateTime TimeIn = DateTime.Today.Add(horaDesde);


                    var prueba = Enumerable.Range(0, (int)(TimeOut - TimeIn).TotalHours)
                    .Select(i => TimeIn.AddHours(i).Hour);


                    var date = DateTime.Now;
                    List<string> horasDisponibles = new List<string>();
                    foreach (var horas in prueba)
                    {
                        TimeSpan result = new TimeSpan((horas) / 100, (horas) % 100, 0);
                        TimeSpan hoy = DateTime.Now.TimeOfDay;

                        horasDisponibles.Add(string.Format("{1:00}:{0:00}", result.Hours, result.Minutes));
                    }

                    horasDisponibles.Add(agenda.IDProfesional.ToString());
                    return Json(horasDisponibles.OrderBy(x => x));//("HorasDisponibles", horasDisponibles.OrderBy(x => x));
                }
                return Json(ErrorEventArgs.Empty);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public JsonResult SaveTurno(DateTime fecha, string tipoTurno, string detalle, int idMascota, int idProfesional,int idEspecialidad,string horario)
        {

            Turno turno = new Turno()
            {
                Fecha = fecha,
                TipoDeTurno = tipoTurno,
                Detalle = detalle,
                IDProfesional = idProfesional, 
                IDEspecialidad = idEspecialidad,
                Horario = horario,
                IDMascota = idMascota
            };
            _context.Add(turno);
            _context.SaveChangesAsync();
           
            return Json(ErrorEventArgs.Empty);
        }
    }
}
