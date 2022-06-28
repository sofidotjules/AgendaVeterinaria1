using AgendaVeterinaria1.Context;
using AgendaVeterinaria1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AgendaVeterinaria1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgendaDBContext _context;

        public HomeController(ILogger<HomeController> logger, AgendaDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> NoAutorizado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost("{controller}")]
        public async Task<IActionResult> Login(string email, string contrasenia) 
        {

            var usuario = _context.Clientes.Where(x => x.Email == email && x.Contrasenia == contrasenia).FirstOrDefault();


            if (usuario is null)
            {
                ViewBag.Error = "Las credenciales ingresadas son incorrectas. Intente nuevamente o comuniquese con Vetagen";
                return View();
            }

            //aca se agrega la session
            HttpContext.Session.SetString("usuario", usuario.IDCliente.ToString());
            /*HttpContext.Session.GetString("usuario")*/

            /*agregar en startup .AddSession(); .UseSession();*/

           // await SignIn(usuario);

            return RedirectToAction("Index", "Home");
        }

        private async Task SignIn(Cliente usuario)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
            if (usuario.Nombre != "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Usuario"));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Profesional"));
            }
            
            identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.IDCliente.ToString()));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }
    }
}