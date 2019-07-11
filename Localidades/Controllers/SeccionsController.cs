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
    public class SeccionsController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: Seccions
        public ActionResult Index()
        {
            var seccions = db.Seccions.Include(s => s.DistritoMunicipal);
            return View(seccions.ToList());
        }

        // GET: Seccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = from r in db.Sectors
                      where r.SeccionId == id
                      select r;

            ViewBag.Datos = res;

            Seccion seccion = db.Seccions.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // GET: Seccions/Create
        public ActionResult Create()
        {
            ViewBag.DistritoMunicipalId = new SelectList(db.DistritoMunicipals, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int iddm)
        {

            var res = from d in db.DistritoMunicipals
                      join s in db.Seccions
                      on d.Id equals s.DistritoMunicipalId
                      where s.DistritoMunicipalId == iddm
                      select s.Id;

            int cant = res.Count();

            var update = from t in db.DistritoMunicipals
                         where t.Id == iddm
                         select t;

            foreach (var item in update)
            {
                item.NumeroSeccion = cant;
            }


            return db;
        }

        // POST: Seccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroSectores,DistritoMunicipalId")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Seccions.Add(seccion);
                db.SaveChanges();

                Actualizar(seccion.DistritoMunicipalId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DistritoMunicipalId = new SelectList(db.DistritoMunicipals, "Id", "Nombre", seccion.DistritoMunicipalId);
            return View(seccion);
        }

        // GET: Seccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccions.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistritoMunicipalId = new SelectList(db.DistritoMunicipals, "Id", "Nombre", seccion.DistritoMunicipalId);
            return View(seccion);
        }

        // POST: Seccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroSectores,DistritoMunicipalId")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistritoMunicipalId = new SelectList(db.DistritoMunicipals, "Id", "Nombre", seccion.DistritoMunicipalId);
            return View(seccion);
        }

        // GET: Seccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccions.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seccion seccion = db.Seccions.Find(id);
            db.Seccions.Remove(seccion);
            db.SaveChanges();

            Actualizar(seccion.DistritoMunicipalId).SaveChanges();

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
