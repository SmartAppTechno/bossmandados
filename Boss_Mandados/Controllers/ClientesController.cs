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

        public struct Mandado
        {
            public string estado;
            public string fecha;
            public string repartidor;
            public string total;
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
            List<Mandado> mandados = new List<Mandado>();
            var mandados_db = db_mandados.manboss_mandados.Where(x => x.cliente == id).ToList();
            foreach(var mandado in mandados_db)
            {
                Mandado aux = new Mandado();
                aux.estado = db_mandados_estados.manboss_mandados_estados.Where(x => x.id == mandado.estado).Select(x => x.nombre).FirstOrDefault();
                aux.fecha = mandado.fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                aux.repartidor = db_repartidores.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = String.Format("{0:C}", mandado.total);
                mandados.Add(aux);
            }
            ViewBag.mandados = mandados;
            return View();
        }
    }
}
