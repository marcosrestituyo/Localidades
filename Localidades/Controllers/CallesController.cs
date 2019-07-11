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
    public class CallesController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: Calles
        public ActionResult Index()
        {
            var calles = db.Calles.Include(c => c.Sector);
            return View(calles.ToList());
        }

        // GET: Calles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = from r in db.Locals
                      where r.CalleId == id
                      select r;

            ViewBag.Datos = res;

            Calle calle = db.Calles.Find(id);
            if (calle == null)
            {
                return HttpNotFound();
            }
            return View(calle);
        }

        // GET: Calles/Create
        public ActionResult Create()
        {
            ViewBag.SectorId = new SelectList(db.Sectors, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int idsec)
        {

            var res = from s in db.Sectors
                      join c in db.Calles
                      on s.Id equals c.SectorId
                      where c.SectorId == idsec
                      select c.Id;

            int cant = res.Count();

            var update = from t in db.Sectors
                         where t.Id == idsec
                         select t;

            foreach (var item in update)
            {
                item.NumeroCalles = cant;
            }


            return db;
        }

        // POST: Calles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroLocales,SectorId")] Calle calle)
        {
            if (ModelState.IsValid)
            {
                db.Calles.Add(calle);
                db.SaveChanges();

                Actualizar(calle.SectorId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SectorId = new SelectList(db.Sectors, "Id", "Nombre", calle.SectorId);
            return View(calle);
        }

        // GET: Calles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calle calle = db.Calles.Find(id);
            if (calle == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectorId = new SelectList(db.Sectors, "Id", "Nombre", calle.SectorId);
            return View(calle);
        }

        // POST: Calles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroLocales,SectorId")] Calle calle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectorId = new SelectList(db.Sectors, "Id", "Nombre", calle.SectorId);
            return View(calle);
        }

        // GET: Calles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calle calle = db.Calles.Find(id);
            if (calle == null)
            {
                return HttpNotFound();
            }
            return View(calle);
        }

        // POST: Calles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calle calle = db.Calles.Find(id);
            db.Calles.Remove(calle);
            db.SaveChanges();

            Actualizar(calle.SectorId).SaveChanges();

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
