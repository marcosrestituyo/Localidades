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
    public class DistritoMunicipalsController : Controller
    {
        private LocalidadesContext db = new LocalidadesContext();

        // GET: DistritoMunicipals
        public ActionResult Index()
        {
            var distritoMunicipals = db.DistritoMunicipals.Include(d => d.Municipio);
            return View(distritoMunicipals.ToList());
        }

        // GET: DistritoMunicipals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = from r in db.Seccions
                      where r.DistritoMunicipalId == id
                      select r;

            ViewBag.Datos = res;

            DistritoMunicipal distritoMunicipal = db.DistritoMunicipals.Find(id);
            if (distritoMunicipal == null)
            {
                return HttpNotFound();
            }
            return View(distritoMunicipal);
        }

        // GET: DistritoMunicipals/Create
        public ActionResult Create()
        {
            ViewBag.MunicipioId = new SelectList(db.Municipios, "Id", "Nombre");
            return View();
        }

        public LocalidadesContext Actualizar(int idmun)
        {

            var res = from m in db.Municipios
                      join d in db.DistritoMunicipals
                      on m.Id equals d.MunicipioId
                      where d.MunicipioId == idmun
                      select d.Id;

            int cant = res.Count();

            var update = from t in db.Municipios
                         where t.Id == idmun
                         select t;

            foreach (var item in update)
            {
                item.NumeroDistritoM = cant;
            }


            return db;
        }

        // POST: DistritoMunicipals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroSeccion,MunicipioId")] DistritoMunicipal distritoMunicipal)
        {
            if (ModelState.IsValid)
            {
                db.DistritoMunicipals.Add(distritoMunicipal);
                db.SaveChanges();

                Actualizar(distritoMunicipal.MunicipioId).SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MunicipioId = new SelectList(db.Municipios, "Id", "Nombre", distritoMunicipal.MunicipioId);
            return View(distritoMunicipal);
        }

        // GET: DistritoMunicipals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistritoMunicipal distritoMunicipal = db.DistritoMunicipals.Find(id);
            if (distritoMunicipal == null)
            {
                return HttpNotFound();
            }
            ViewBag.MunicipioId = new SelectList(db.Municipios, "Id", "Nombre", distritoMunicipal.MunicipioId);
            return View(distritoMunicipal);
        }

        // POST: DistritoMunicipals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroSeccion,MunicipioId")] DistritoMunicipal distritoMunicipal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distritoMunicipal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MunicipioId = new SelectList(db.Municipios, "Id", "Nombre", distritoMunicipal.MunicipioId);
            return View(distritoMunicipal);
        }

        // GET: DistritoMunicipals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistritoMunicipal distritoMunicipal = db.DistritoMunicipals.Find(id);
            if (distritoMunicipal == null)
            {
                return HttpNotFound();
            }
            return View(distritoMunicipal);
        }

        // POST: DistritoMunicipals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DistritoMunicipal distritoMunicipal = db.DistritoMunicipals.Find(id);
            db.DistritoMunicipals.Remove(distritoMunicipal);
            db.SaveChanges();

            Actualizar(distritoMunicipal.MunicipioId).SaveChanges();

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
