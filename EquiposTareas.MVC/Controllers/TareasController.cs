using EquiposProyectosApi.Consumer;
using EquiposTareas.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoEquiposs;

namespace EquiposTareas.MVC.Controllers
{
 
    public class TareasController : Controller
    {
        // GET: TareasController
        public ActionResult Index()
        {
            var data = Crud<Tarea>.GetAll();
            return View(data);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Tarea>.GetById(id);

            return View(data);
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            ViewBag.Tareas = GetTareas();

            return View();
        }
        private List<SelectListItem> GetTareas()
        {
            var tareas = Crud<Tarea>.GetAll();
            return tareas.Select(p => new SelectListItem
            {
                Value = p.id.ToString(),
                Text = p.Estado
            }).ToList();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea tarea)
        {
            try
            {


                Crud<Tarea>.Create(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Tarea>.GetById(id);
            ViewBag.Tareas = GetTareas();
            return View(data);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Update(id, tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Tarea>.GetById(id);
            return View(data);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }
    }
    }
