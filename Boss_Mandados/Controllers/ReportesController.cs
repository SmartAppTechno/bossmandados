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
        private RepartidoresEntities db_repartidores = new RepartidoresEntities();
        private RepartidoresCalificacionesEntities db_calificaciones = new RepartidoresCalificacionesEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();

        public struct Promocion
        {
            public string nombre;
            public string descripcion;
            public string fecha_inicio;
            public string fecha_fin;
            public int veces;
        }

        public struct Repartidor
        {
            public string nombre;
            public double rating;
            public double efectivo;
        }

        // GET: Promociones
        public ActionResult Promociones()
        {
            List<Promocion> promociones = new List<Promocion>();
            if (Request.Form["fecha_inicio"] != null && Request.Form["fecha_fin"] != null)
            {
                DateTime fi = Convert.ToDateTime(Request.Form["fecha_inicio"]);
                DateTime ff = Convert.ToDateTime(Request.Form["fecha_fin"]);
                var promocion_db = db.manboss_promociones.Where(x => x.fecha_inicio >= fi && x.fecha_fin <= ff).ToList();
                int total = 0;
                foreach (var promocion in promocion_db)
                {
                    Promocion aux = new Promocion();
                    aux.nombre = promocion.nombre;
                    aux.descripcion = promocion.descripcion;
                    aux.fecha_inicio = promocion.fecha_inicio.ToString("dd/MM/yyyy");
                    aux.fecha_fin = promocion.fecha_fin.ToString("dd/MM/yyyy");
                    aux.veces = db_prom_clientes.manboss_promociones_clientes.Where(x => x.promocion == promocion.id).Count();
                    total += aux.veces;
                    promociones.Add(aux);
                }
                ViewBag.total_promociones = total;
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
            }
            ViewBag.promociones = promociones;
            return View();
        }

        // GET: Repartidores
        public ActionResult Repartidores()
        {
            List<Repartidor> repartidores = new List<Repartidor>();
            var repartidor_db = db_repartidores.manboss_repartidores.ToList();
            double rating_total = 0;
            double rep = 0;
            double efectivo_total = 0;
            foreach (var repartidor in repartidor_db)
            {
                Repartidor aux = new Repartidor();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).Select(x => x.nombre).FirstOrDefault();
                double total = db_calificaciones.manboss_repartidores_calificaciones.Where(x => x.repartidor == repartidor.id).Sum(x => x.calificacion);
                int pedidos = db_calificaciones.manboss_repartidores_calificaciones.Where(x => x.repartidor == repartidor.id).Count();
                aux.rating = efectivo_total / pedidos;
                aux.efectivo = repartidor.efectivo;
                efectivo_total += aux.efectivo;
                rating_total += aux.rating;
                rep++;
                repartidores.Add(aux);
            }
            ViewBag.rating_total = rating_total / rep;
            ViewBag.efectivo_total = efectivo_total;
            ViewBag.repartidores = repartidores;
            return View();
        }
    }
}