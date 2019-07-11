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
    public class SectorsController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: Sectors
        public ActionResult Index()
        {
            var sectors = db.Sectors.Include(s => s.Seccion);
            return View(sectors.ToList());
        }

        // GET: Sectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = from r in db.Calles
                      where r.SectorId == id
                      select r;

            ViewBag.Datos = res;

            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // GET: Sectors/Create
        public ActionResult Create()
        {
            ViewBag.SeccionId = new SelectList(db.Seccions, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int idsec)
        {

            var res = from s in db.Seccions
                      join se in db.Sectors
                      on s.Id equals se.SeccionId
                      where se.SeccionId == idsec
                      select se.Id;

            int cant = res.Count();

            var update = from t in db.Seccions
                         where t.Id == idsec
                         select t;

            foreach (var item in update)
            {
                item.NumeroSectores = cant;
            }


            return db;
        }

        // POST: Sectors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroCalles,SeccionId")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Sectors.Add(sector);
                db.SaveChanges();

                Actualizar(sector.SeccionId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SeccionId = new SelectList(db.Seccions, "Id", "Nombre", sector.SeccionId);
            return View(sector);
        }

        // GET: Sectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeccionId = new SelectList(db.Seccions, "Id", "Nombre", sector.SeccionId);
            return View(sector);
        }

        // POST: Sectors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroCalles,SeccionId")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeccionId = new SelectList(db.Seccions, "Id", "Nombre", sector.SeccionId);
            return View(sector);
        }

        // GET: Sectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sector sector = db.Sectors.Find(id);
            db.Sectors.Remove(sector);
            db.SaveChanges();

            Actualizar(sector.SeccionId).SaveChanges();

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
