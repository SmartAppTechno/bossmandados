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
    public class ClientesController : Controller
    {
        private ClientesEntities db = new ClientesEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.manboss_clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_clientes manboss_clientes = db.manboss_clientes.Find(id);
            if (manboss_clientes == null)
            {
                return HttpNotFound();
            }
            return View(manboss_clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cliente,red_social,direccion,latitud,longitud,telefono")] manboss_clientes manboss_clientes)
        {
            db.manboss_clientes.Add(manboss_clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_clientes manboss_clientes = db.manboss_clientes.Find(id);
            if (manboss_clientes == null)
            {
                return HttpNotFound();
            }
            return View(manboss_clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cliente,red_social,direccion,latitud,longitud,telefono")] manboss_clientes manboss_clientes)
        {
            db.Entry(manboss_clientes).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manboss_clientes manboss_clientes = db.manboss_clientes.Find(id);
            if (manboss_clientes == null)
            {
                return HttpNotFound();
            }
            return View(manboss_clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manboss_clientes manboss_clientes = db.manboss_clientes.Find(id);
            db.manboss_clientes.Remove(manboss_clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
