using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class RepartidoresController : Controller
    {
        private RepartidoresEntities db = new RepartidoresEntities();

        // GET: Repartidores
        public ActionResult Index()
        {
            return View(db.manboss_repartidores.ToList());
        }

        // GET: Repartidores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_repartidores manboss_repartidores = db.manboss_repartidores.Find(id);
            if (manboss_repartidores == null)
            {
                return HttpNotFound();
            }
            return View(manboss_repartidores);
        }

        // GET: Repartidores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repartidores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,repartidor,latitud,longitud,rating,efectivo")] manboss_repartidores manboss_repartidores)
        {
            db.manboss_repartidores.Add(manboss_repartidores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Repartidores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_repartidores manboss_repartidores = db.manboss_repartidores.Find(id);
            if (manboss_repartidores == null)
            {
                return HttpNotFound();
            }
            return View(manboss_repartidores);
        }

        // POST: Repartidores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,repartidor,latitud,longitud,rating,efectivo")] manboss_repartidores manboss_repartidores)
        {
            db.Entry(manboss_repartidores).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Repartidores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_repartidores manboss_repartidores = db.manboss_repartidores.Find(id);
            if (manboss_repartidores == null)
            {
                return HttpNotFound();
            }
            return View(manboss_repartidores);
        }

        // POST: Repartidores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manboss_repartidores manboss_repartidores = db.manboss_repartidores.Find(id);
            db.manboss_repartidores.Remove(manboss_repartidores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
