using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class ReportesController : Controller
    {
        private PromocionesEntities db = new PromocionesEntities();
        private PromocionesClientesEntities db_prom_clientes = new PromocionesClientesEntities();

        public struct Promocion
        {
            public string nombre;
            public string descripcion;
            public string fecha_inicio;
            public string fecha_fin;
            public int veces;
        }

        // GET: Promociones
        public ActionResult Promociones()
        {
            List<Promocion> promociones = new List<Promocion>();
            if (Request.Form["fecha_inicio"] != null && Request.Form["fecha_fin"] != null)
            {
                DateTime fi = Convert.ToDateTime(Request.Form["fecha_inicio"]);
                DateTime ff = Convert.ToDateTime(Request.Form["fecha_fin"]);
                var promocion_db = db.manboss_promociones.Where(x=> x.fecha_inicio >= fi).ToList();
                foreach (var promocion in promocion_db)
                {
                    Promocion aux = new Promocion();
                    aux.nombre = promocion.nombre;
                    aux.descripcion = promocion.descripcion;
                    aux.fecha_inicio = promocion.fecha_inicio.ToString("dd/MM/yyyy");
                    aux.fecha_fin = promocion.fecha_fin.ToString("dd/MM/yyyy");
                    aux.veces = db_prom_clientes.manboss_promociones_clientes.Where(x => x.promocion == promocion.id).Count();
                    promociones.Add(aux);
                }
                ViewBag.estado = "busqueda";
            }
            else
            {
                var promocion_db = db.manboss_promociones.ToList();
                foreach (var promocion in promocion_db)
                {
                    Promocion aux = new Promocion();
                    aux.nombre = promocion.nombre;
                    aux.descripcion = promocion.descripcion;
                    aux.fecha_inicio = promocion.fecha_inicio.ToString("dd/MM/yyyy");
                    aux.fecha_fin = promocion.fecha_fin.ToString("dd/MM/yyyy");
                    aux.veces = db_prom_clientes.manboss_promociones_clientes.Where(x => x.promocion == promocion.id).Count();
                    promociones.Add(aux);
                }
                ViewBag.estado = "todo";
            }
            ViewBag.promociones = promociones;
            return View();
        }
    }
}