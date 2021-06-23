using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2.Models;

namespace ASP2.Controllers
{
    public class productoCompraController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static string totalCompra(int? idCompra)
        {
            using (var db = new inventarioEntities())
            {
                return Convert.ToString(db.compra.Find(idCompra).total);
            }
        }

        public ActionResult listarCompra()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.compra.ToList());
            }
        }

        public static string nombreProducto(int? idProducto)
        {
            using (var db = new inventarioEntities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult listarProducto()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra proCompra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto_compra.Add(proCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error {Ex}");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities())
            {
                var proCompraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(proCompraEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto_compra proCompraEdit)
        {

            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities())
                {
                    var oldproCompra = db.producto_compra.Find(proCompraEdit.id);
                    oldproCompra.id_compra = proCompraEdit.id_compra;
                    oldproCompra.id_producto = proCompraEdit.id_producto;
                    oldproCompra.cantidad = proCompraEdit.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error{Ex}");
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var proCompra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(proCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error {Ex}");
                return View();
            }
        }
    }
}