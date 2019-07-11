using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Localidades.Models;

namespace Localidades.Controllers
{
    public class LocalsController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: Locals
        public ActionResult Index()
        {
            var locals = db.Locals.Include(l => l.Calle);
            return View(locals.ToList());
        }

        // GET: Locals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals/Create
        public ActionResult Create()
        {
            ViewBag.CalleId = new SelectList(db.Calles, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int idca)
        {

            var res = from c in db.Calles
                      join l in db.Locals
                      on c.Id equals l.CalleId
                      where l.CalleId == idca
                      select l.Id;

            int cant = res.Count();

            var update = from t in db.Calles
                         where t.Id == idca
                         select t;

            foreach (var item in update)
            {
                item.NumeroLocales = cant;
            }


            return db;
        }

        // POST: Locals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,CalleId")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Locals.Add(local);
                db.SaveChanges();

                Actualizar(local.CalleId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CalleId = new SelectList(db.Calles, "Id", "Nombre", local.CalleId);
            return View(local);
        }

        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.CalleId = new SelectList(db.Calles, "Id", "Nombre", local.CalleId);
            return View(local);
        }

        // POST: Locals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,CalleId")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CalleId = new SelectList(db.Calles, "Id", "Nombre", local.CalleId);
            return View(local);
        }

        // GET: Locals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
            db.SaveChanges();

            Actualizar(local.CalleId).SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
