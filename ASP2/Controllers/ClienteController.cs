using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2.Models;

namespace ASP2.Views
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.cliente.ToList());

            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(cliente cliente)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.cliente.Add(cliente);

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
                var findCustomer = db.cliente.Find(id);
                return View(findCustomer);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findCustomer = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCustomer);
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

        public ActionResult Edit(cliente editCustomer)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var customer = db.cliente.Find(editCustomer.id);

                    customer.nombre = editCustomer.nombre;
                    customer.documento = editCustomer.documento;
                    customer.email = editCustomer.email;
                    

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
                    var findCustomer = db.cliente.Find(id);
                    db.cliente.Remove(findCustomer);
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