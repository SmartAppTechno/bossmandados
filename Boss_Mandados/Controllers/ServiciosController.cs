using System.Linq;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class ServiciosController : Controller
    {
        private ServiciosEntities db = new ServiciosEntities();

        // GET: Servicios
        public ActionResult Index()
        {
            return View(db.manboss_servicios.ToList());
        }

        // GET: Servicios/Details/5
        public ActionResult Details(int? id)
        {
            manboss_servicios manboss_servicios = db.manboss_servicios.Find(id);
            return View(manboss_servicios);
        }

        // GET: Servicios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,tarifa_base_ex,costo_minuto_ex,costo_km_ex,tarifa_base_co,costo_minuto_co,costo_km_co,foto")] manboss_servicios manboss_form)
        {
            db.manboss_servicios.Add(manboss_form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            manboss_servicios manboss_servicios = db.manboss_servicios.Find(id);
            return View(manboss_servicios);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,tarifa_base_ex,costo_minuto_ex,costo_km_ex,tarifa_base_co,costo_minuto_co,costo_km_co,foto")] manboss_servicios servicio_form)
        {
            var servicio_actual = db.manboss_servicios.FirstOrDefault(x => x.id == servicio_form.id);
            servicio_actual.nombre = servicio_form.nombre;
            servicio_actual.tarifa_base_ex = servicio_form.tarifa_base_ex;
            servicio_actual.costo_minuto_ex = servicio_form.costo_minuto_ex;
            servicio_actual.costo_km_ex = servicio_form.costo_km_ex;
            servicio_actual.tarifa_base_co = servicio_form.tarifa_base_co;
            servicio_actual.costo_minuto_co = servicio_form.costo_minuto_co;
            servicio_actual.costo_km_co = servicio_form.costo_km_co;
            if (!string.IsNullOrEmpty(servicio_form.foto))
            {
                servicio_actual.foto = servicio_form.foto;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            manboss_servicios manboss_servicios = db.manboss_servicios.Find(id);
            return View(manboss_servicios);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manboss_servicios manboss_servicios = db.manboss_servicios.Find(id);
            db.manboss_servicios.Remove(manboss_servicios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}