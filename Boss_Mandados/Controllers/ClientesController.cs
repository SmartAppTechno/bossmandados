using System.Linq;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Boss_Mandados.Controllers
{
    public class ClientesController : Controller
    {
        private ClientesEntities db = new ClientesEntities();
        private DireccionesEntities db_direcciones = new DireccionesEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosEstadosEntities db_mandados_estados = new MandadosEstadosEntities();
        private UsuariosEntities db_repartidores = new UsuariosEntities();
        private MandadosRutasEntities db_mandados_rutas = new MandadosRutasEntities();
        private ServiciosEntities db_servicios = new ServiciosEntities();

        public struct Mandado_object
        {
            public int id;
            public string estado;
            public string fecha;
            public string repartidor;
            public string total;
        }

        public struct Mandado_detalle
        {
            public string servicio;
            public string comentarios;
            public float latitud;
            public float longitud;
        }

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.manboss_clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            manboss_clientes cliente = db.manboss_clientes.Find(id);
            return View(cliente);
        }

        public ActionResult Direcciones(int? id)
        {
            ViewBag.direcciones = db_direcciones.manboss_clientes_direcciones.Where(x => x.cliente == id).ToList();
            return View();
        }

        public ActionResult Mandados(int? id)
        {
            List<Mandado_object> mandados = new List<Mandado_object>();
            var mandados_db = db_mandados.manboss_mandados.Where(x => x.cliente == id).ToList();
            foreach(var mandado in mandados_db)
            {
                Mandado_object aux = new Mandado_object();
                aux.id = mandado.id;
                aux.estado = db_mandados_estados.manboss_mandados_estados.Where(x => x.id == mandado.estado).Select(x => x.nombre).FirstOrDefault();
                aux.fecha = mandado.fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                aux.repartidor = db_repartidores.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = String.Format("{0:C}", mandado.total);
                mandados.Add(aux);
            }
            ViewBag.mandados = mandados;
            return View();
        }
        public ActionResult Mandado(int? id)
        {
            List<Mandado_detalle> mandados = new List<Mandado_detalle>();
            var mandados_db = db_mandados_rutas.manboss_mandados_rutas.Where(x => x.mandado == id).ToList();
            foreach (var mandado in mandados_db)
            {
                Mandado_detalle aux = new Mandado_detalle();
                aux.servicio = db_servicios.manboss_servicios.Where(x => x.id == mandado.servicio).Select(x => x.nombre).FirstOrDefault();
                aux.comentarios = mandado.comentarios;
                aux.latitud = (float) mandado.latitud;
                aux.longitud = (float) mandado.longitud;
                mandados.Add(aux);
            }
            ViewBag.mandado = mandados;
            ViewBag.mandado_id = db_mandados.manboss_mandados.Where(x=>x.id == id).Select(x=> x.cliente).FirstOrDefault();
            return View();
        }
    }
}
