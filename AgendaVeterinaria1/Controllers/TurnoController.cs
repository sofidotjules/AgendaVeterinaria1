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
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = _context.Turnos.Include(x => x.Mascota).FirstOrDefault(x => x.IDTurno == id);

            if (turno == null)
            {
                return NotFound();
            }
            var cliente = _context.Clientes.Where(x => x.Mascotas.Any(x => x.IDMascota == turno.IDMascota)).FirstOrDefault();

            ViewData["Cliente"] = cliente.Nombre + " - Contacto: " + cliente.Email;
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
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'AgendaDBContext.Turnos'  is null.");
            }
            var turno = _context.Turnos.Find(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AgendaTurnos(int especialidadId=0)
        {
            var usuario = HttpContext.Session.GetString("usuario");
            var tipoUsuario = HttpContext.Session.GetString("tipoUsuario");

            if (usuario == null || !tipoUsuario.Equals("Admin"))
            {
                return RedirectToAction("Login", "Home");
            }


            var profesional = _context.Profesionales.Include(x => x.Especialidades).Where(x => x.IDProfesional == Convert.ToInt32(usuario)).FirstOrDefault();
            var agendaDBContext = _context.Turnos.Include(t => t.Mascota).Include(t => t.Profesional).Include(x => x.Especialidad).Where(x => x.IDProfesional == Convert.ToInt32(usuario));
            if (especialidadId !=0 )
            {
                agendaDBContext = _context.Turnos.Include(t => t.Mascota).Include(t => t.Profesional).Include(x => x.Especialidad).Where(x => x.IDProfesional == Convert.ToInt32(usuario) && x.IDEspecialidad==especialidadId);
            }
            
            ViewData["Especialidades"] = profesional.Especialidades;
            ViewBag.Id = Convert.ToInt32(usuario);
            
            return View(await agendaDBContext.ToListAsync());

            //return View();
        }

        // POST: Turno/Delete/5
        [HttpPost]
        public IActionResult AgendaTurnos()
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'AgendaDBContext.Turnos'  is null.");
            }
          //  var turno = _context.Turnos.Find(id);
           // if (turno != null)
          //  {
          //      _context.Turnos.Remove(turno);
          //  }

           // _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
          return (_context.Turnos?.Any(e => e.IDTurno == id)).GetValueOrDefault();
        }


        [AllowAnonymous]
        public IActionResult SolicitarTurno(string? tipoTurno)
        {
            var usuario = HttpContext.Session.GetString("usuario");
            var tipoUsuario = HttpContext.Session.GetString("tipoUsuario");

            if (usuario == null || tipoUsuario.Equals("Admin")) {
                return RedirectToAction("Login", "Home");
            }
        

            ViewBag.TipoTurno = tipoTurno;
            var cliente = _context.Clientes.Include(x=>x.Mascotas).Where(x => x.IDCliente == Convert.ToInt32(usuario)).FirstOrDefault();
            ViewData["Mascotas"] = cliente.Mascotas;
            ViewData["Especialidades"] = _context.Especialidades;
           
            return View();
        }

        public JsonResult ObtenerHorasTurno(DateTime fecha, int idEspecialidad, string tipoTurno) 
        {
            try
            {
                /*consultar si los turnos ya fueron s olicitados en ese horario*/
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

                        var horasACargar = string.Format("{1:00}:{0:00}", result.Hours, result.Minutes);
                        List<Turno> turnoList = _context.Turnos.Where(x=>x.Fecha==fecha && x.Horario ==horasACargar).ToList();
                        if (turnoList.Count==0)
                        {
                            horasDisponibles.Add(horasACargar);
                        }                        
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

        [AllowAnonymous]
        public JsonResult SaveTurno(DateTime fecha, string tipoTurno, string detalle, int idMascota, int idProfesional,int idEspecialidad,string horario)
        {
            try
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
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
           
            return Json(ErrorEventArgs.Empty);
        }
    }
}
