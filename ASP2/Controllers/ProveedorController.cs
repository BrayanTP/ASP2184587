using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2.Models;

namespace ASP2.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.proveedor.ToList());

            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor proveedor)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.proveedor.Add(proveedor);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"error {ex}");
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                var findSupplier = db.proveedor.Find(id);
                return View(findSupplier);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findSupplier = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findSupplier);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"error {ex}");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(proveedor editSupplier)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var supplier = db.proveedor.Find(editSupplier.id);

                    supplier.nombre = editSupplier.nombre;
                    supplier.direccion = editSupplier.direccion;
                    supplier.telefono = editSupplier.telefono;
                    supplier.nombre_contacto = editSupplier.nombre_contacto;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"error {ex}");
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findSupplier = db.proveedor.Find(id);
                    db.proveedor.Remove(findSupplier);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"error {ex}");
                return View();
            }
        }


    }
}