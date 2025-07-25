using EquiposProyectosApi.Consumer;
using EquiposTareas.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoEquiposs;

namespace EquiposTareas.MVC.Controllers
{
  
    public class ProyectosController : Controller
    { // GET: ProyectosController
        public ActionResult Index()
        {
            var data = Crud<Proyecto>.GetAll();
            return View(data);
        }

        // GET: ProyectosController/Details/5
        public ActionResult Details(int id)
        {

            var data = Crud<Proyecto>.GetById(id);

            return View(data);
        }

        // GET: ProyectosController/Create
        public ActionResult Create()
        {
            ViewBag.Proyectos = GetProyectos();

            return View();
        }
        private List<SelectListItem> GetProyectos()
        {
            var paises = Crud<Proyecto>.GetAll();
            return paises.Select(p => new SelectListItem
            {
                Value = p.id.ToString(),
                Text = p.nombre
            }).ToList();
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proyecto proyecto)
        {
            try
            {


                Crud<Proyecto>.Create(proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(proyecto);
            }
        }

        // GET: ProyectosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            ViewBag.Proyectos = GetProyectos();
            return View(data);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proyecto proyecto)
        {
            try
            {
                Crud<Proyecto>.Update(id, proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(proyecto);
            }
        }


        // GET: ProyectosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            return View(data);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proyecto proyecto)
        {
            try
            {
                Crud<Proyecto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(proyecto);
            }
        }
    }
}
