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
    public class ProvinciasController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: Provincias
        public ActionResult Index()
        {
            var provincias = db.Provincias.Include(p => p.Region);
            return View(provincias.ToList());
        }

        // GET: Provincias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = from r in db.Municipios
                      where r.ProvinciaId == id
                      select r;

            ViewBag.Datos = res;

            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // GET: Provincias/Create
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int idreg)
        {

            var res = from r in db.Regions
                         join p in db.Provincias
                         on r.Id equals p.RegionId
                         where p.RegionId == idreg
                         select p.Id;

            int cant = res.Count();

            var update = from t in db.Regions
                        where t.Id == idreg
                        select t;

            foreach (var item in update)
            {
                item.NumeroProvincias = cant;
            }


            return db;
        }


        // POST: Provincias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroMunicipios,RegionId")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                db.Provincias.Add(provincia);
                db.SaveChanges();

                Actualizar(provincia.RegionId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Nombre", provincia.RegionId);
            return View(provincia);
        }

        // GET: Provincias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Nombre", provincia.RegionId);
            return View(provincia);
        }

        // POST: Provincias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroMunicipios,RegionId")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provincia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Nombre", provincia.RegionId);
            return View(provincia);
        }

        // GET: Provincias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provincia provincia = db.Provincias.Find(id);
            db.Provincias.Remove(provincia);
            db.SaveChanges();

            Actualizar(provincia.RegionId).SaveChanges();

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
