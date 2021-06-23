using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2.Models;
using System.IO;

namespace ASP2.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor

        [Authorize]
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

            if (!ModelState.IsValid)
                return View();

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

        public ActionResult uploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            string filePath = string.Empty;

            if(fileForm != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);
                fileForm.SaveAs(filePath);

                string CSVdata = System.IO.File.ReadAllText(filePath);

                foreach(string row in CSVdata.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newProveedor = new proveedor
                        {
                            nombre = row.Split(';')[0],
                            nombre_contacto = row.Split(';')[1],
                            direccion = row.Split(';')[2],
                            telefono = row.Split(';')[3],
                        };

                        using (var db = new inventarioEntities())
                        {
                            db.proveedor.Add(newProveedor);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}