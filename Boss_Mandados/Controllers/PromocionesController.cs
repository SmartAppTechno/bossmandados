using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class PromocionesController : Controller
    {
        private PromocionesEntities db = new PromocionesEntities();

        // GET: Promociones
        public ActionResult Index()
        {
            return View(db.manboss_promociones.ToList());
        }

        // GET: Promociones/Details/5
        public ActionResult Details(int? id)
        {
            manboss_promociones manboss_promociones = db.manboss_promociones.Find(id);
            return View(manboss_promociones);
        }

        // GET: Promociones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promociones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,descuento,cupon,fecha_inicio,fecha_fin")] manboss_promociones manboss_promociones)
        {
            db.manboss_promociones.Add(manboss_promociones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Promociones/Edit/5
        public ActionResult Edit(int? id)
        {
            manboss_promociones manboss_promociones = db.manboss_promociones.Find(id);
            return View(manboss_promociones);
        }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,descuento,cupon,fecha_inicio,fecha_fin")] manboss_promociones manboss_promociones)
        {
            db.Entry(manboss_promociones).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Promociones/Delete/5
        public ActionResult Delete(int? id)
        {
            manboss_promociones manboss_promociones = db.manboss_promociones.Find(id);
            return View(manboss_promociones);
        }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manboss_promociones manboss_promociones = db.manboss_promociones.Find(id);
            db.manboss_promociones.Remove(manboss_promociones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
