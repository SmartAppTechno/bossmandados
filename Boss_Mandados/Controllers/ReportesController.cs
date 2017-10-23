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
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosEstadosEntities db_estados = new MandadosEstadosEntities();
        private ComisionesEntities db_comisiones = new ComisionesEntities();

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

        public struct Mandado
        {
            public string estado;
            public string cliente;
            public string repartidor;
            public double total;
            public string fecha;
        }

        public struct Comision
        {
            public string repartidor;
            public string fecha;
            public double total;
            public double comision;
            public string tipo_pago;
        }

        public struct Empleado
        {
            public int id;
            public string nombre;
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
                aux.rating = total / pedidos;
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

        // GET: Mandados
        public ActionResult Mandados()
        {
            List<Mandado> mandados = new List<Mandado>();
            if (Request.Form["fecha"] != null && Request.Form["estado"] != null)
            {
                DateTime fecha = Convert.ToDateTime(Request.Form["fecha"]);
                int estado = Int32.Parse(Request.Form["estado"]);
                var mandado_db = db_mandados.manboss_mandados.Where(x => x.fecha == fecha && x.estado == estado).ToList();
                double total = 0;
                foreach (var mandado in mandado_db)
                {
                    Mandado aux = new Mandado();
                    aux.estado = db_estados.manboss_mandados_estados.Where(x => x.id == mandado.estado).Select(x => x.nombre).FirstOrDefault();
                    aux.cliente = db_usuarios.manboss_usuarios.Where(x=> x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                    aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                    aux.total = mandado.total;
                    aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                    total += aux.total;
                    mandados.Add(aux);
                }
                ViewBag.total_mandados = total;
            }
            else
            {
                var mandado_db = db_mandados.manboss_mandados.ToList();
                double total = 0;
                foreach (var mandado in mandado_db)
                {
                    Mandado aux = new Mandado();
                    aux.estado = db_estados.manboss_mandados_estados.Where(x => x.id == mandado.estado).Select(x => x.nombre).FirstOrDefault();
                    aux.cliente = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                    aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                    aux.total = mandado.total;
                    aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                    total += aux.total;
                    mandados.Add(aux);
                }
                ViewBag.total_mandados = total;
            }
            ViewBag.mandados = mandados;
            return View();
        }

        // GET: Comisiones
        public ActionResult Comisiones()
        {
            List<Comision> comisiones = new List<Comision>();
            double pagar = 0;
            double deben = 0;
            //Repartidor
            if (Request.Form["repartidor"] != null)
            {
                DateTime fi = Convert.ToDateTime(Request.Form["fecha_inicio"]);
                DateTime ff = Convert.ToDateTime(Request.Form["fecha_fin"]);
                int repatidor_id = Int32.Parse(Request.Form["repartidor"]);
                var mandados_db = db_mandados.manboss_mandados.Where(x => x.fecha >= fi && x.fecha <= ff && x.repartidor == repatidor_id).ToList();
                foreach (var mandado in mandados_db)
                {
                    Comision aux = new Comision();
                    aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                    aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                    aux.total = mandado.total;
                    aux.comision = db_comisiones.manboss_comisiones.Where(x => x.mandado == mandado.id).Select(x => x.comision).FirstOrDefault();
                    if(mandado.tipo_pago == 0)
                    {
                        aux.tipo_pago = "Efectivo";
                        double temp = aux.total - aux.comision;
                        deben += temp;
                    }
                    if (mandado.tipo_pago == 1)
                    {
                        aux.tipo_pago = "Paypal";
                        pagar += aux.comision;
                    }
                    comisiones.Add(aux);
                }
            }
            //Sin Repartidor
            else if (Request.Form["repartidor"] == null)
            {
                DateTime fi = Convert.ToDateTime(Request.Form["fecha_inicio"]);
                DateTime ff = Convert.ToDateTime(Request.Form["fecha_fin"]);
                var mandados_db = db_mandados.manboss_mandados.Where(x => x.fecha >= fi && x.fecha <= ff).ToList();
                foreach (var mandado in mandados_db)
                {
                    Comision aux = new Comision();
                    aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                    aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                    aux.total = mandado.total;
                    aux.comision = db_comisiones.manboss_comisiones.Where(x => x.mandado == mandado.id).Select(x => x.comision).FirstOrDefault();
                    if (mandado.tipo_pago == 0)
                    {
                        aux.tipo_pago = "Efectivo";
                        double temp = aux.total - aux.comision;
                        deben += temp;
                    }
                    if (mandado.tipo_pago == 1)
                    {
                        aux.tipo_pago = "Paypal";
                        pagar += aux.comision;
                    }
                    comisiones.Add(aux);
                }
            }
            ViewBag.comisiones = comisiones;
            ViewBag.pagar = pagar;
            ViewBag.deben = deben;
            //Agregar la lista de repartidores para la búsqueda
            var repartidor_id = db_repartidores.manboss_repartidores.ToList();
            List<Empleado> repartidores = new List<Empleado>();
            foreach (var item in repartidor_id)
            {
                var repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == item.repartidor).FirstOrDefault();
                Empleado aux = new Empleado();
                aux.id = repartidor.id;
                aux.nombre = repartidor.nombre;
                repartidores.Add(aux);
            }
            ViewBag.repartidores = repartidores;
            return View();
        }
    }
}