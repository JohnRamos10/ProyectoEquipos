using EquiposProyectosApi.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoEquiposs;

namespace EquiposTareas.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IActionResult Index(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Index(LoginViewModel model, string role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Role = role;
                return View(model);
            }

            var usuarios = Crud<UsuarioLog>.GetAll();

            // Buscar usuario con ese correo y clave
            var usuario = usuarios.FirstOrDefault(u => u.Correo == model.Correo && u.Clave == model.Clave);

            if (usuario == null)
            {
                ViewBag.Role = role;
                ViewBag.Error = "Usuario o clave incorrectos.";
                return View(model);
            }

            // Validar que el rol coincida
            if (!string.Equals(usuario.Rol, role, System.StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Role = role;

                if (usuario.Rol == "Administrador" && role == "Cliente")
                {
                    ViewBag.Error = "Este correo está autenticado como Administrador. No puede iniciar sesión como Cliente.";
                }
                else if (usuario.Rol == "Cliente" && role == "Administrador")
                {
                    ViewBag.Error = "Este correo no está autorizado como Administrador.";
                }
                else
                {
                    ViewBag.Error = "Rol incorrecto para este usuario.";
                }

                return View(model);
            }

            // Guardar en sesión
            HttpContext.Session.SetString("Usuario", usuario.Correo);
            HttpContext.Session.SetString("Rol", usuario.Rol);

            TempData["Success"] = $"Sesión iniciada como {usuario.Correo} ({usuario.Rol})";

            // Redirigir según rol
            if (usuario.Rol == "Administrador")
                return RedirectToAction("Index", "Clientes");

            // Validar que también exista en la tabla Cliente
            var cliente = Crud<Usuario>.GetAll().FirstOrDefault(c => c.Correo == usuario.Correo);
            if (cliente == null)
            {
                TempData["Error"] = "Este usuario tiene rol 'Cliente', pero no está registrado como Cliente en el sistema.";
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Index", "Boletos");
        }

        // GET: Registro solo para Cliente
        public IActionResult Register()
        {
            return View();
        }

        // POST: Registro solo para Cliente
        [HttpPost]
        public IActionResult Register(UsuarioLog model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Rol = "Cliente"; // Forzar rol Cliente

            var usuarios = Crud<Usuario>.GetAll();

            if (usuarios.Any(u => u.Correo == model.Correo))
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                return View(model);
            }

            try
            {
                Crud<UsuarioLog>.Create(model);

                // Crear el cliente en tabla Clientes automáticamente
                var nuevoCliente = new Usuario
                {
                    nombre = model.Nombre,
                    Correo = model.Correo
                };
                Crud<Usuario>.Create(nuevoCliente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear usuario o cliente: " + ex.Message);
                return View(model);
            }

            TempData["Success"] = "Registro exitoso. Ahora puede iniciar sesión.";
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Sesión cerrada.";
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult SelectRole()
        {
            return View();
        }
    }


}
