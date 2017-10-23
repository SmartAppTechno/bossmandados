using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class MandadosController : Controller
    {
        private RepartidoresEntities db_repartidores = new RepartidoresEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosEstadosEntities db_estados = new MandadosEstadosEntities();
        private MandadosRutasEntities db_rutas = new MandadosRutasEntities();

        public struct Mandado
        {
            public string cliente;
            public string repartidor;
            public double total;
            public string fecha;
        }

        // GET: Mandados
        public ActionResult Index()
        {
            //En Espera
            List<Mandado> espera = new List<Mandado>();
            var espera_db = db_mandados.manboss_mandados.Where(x => x.estado == 1).ToList();
            foreach (var mandado in espera_db)
            {
                Mandado aux = new Mandado();
                aux.cliente = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                espera.Add(aux);
            }
            ViewBag.mandados_espera = espera;
            //Asignado
            List<Mandado> asignado = new List<Mandado>();
            var asignado_db = db_mandados.manboss_mandados.Where(x => x.estado == 2).ToList();
            foreach (var mandado in asignado_db)
            {
                Mandado aux = new Mandado();
                aux.cliente = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                asignado.Add(aux);
            }
            ViewBag.mandados_asignado = asignado;
            //En Proceso
            List<Mandado> proceso = new List<Mandado>();
            var proceso_db = db_mandados.manboss_mandados.Where(x => x.estado == 3).ToList();
            foreach (var mandado in proceso_db)
            {
                Mandado aux = new Mandado();
                aux.cliente = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                proceso.Add(aux);
            }
            ViewBag.mandados_proceso = proceso;
            //Entregado
            List<Mandado> entregado = new List<Mandado>();
            var entregado_db = db_mandados.manboss_mandados.Where(x => x.estado == 4).ToList();
            foreach (var mandado in entregado_db)
            {
                Mandado aux = new Mandado();
                aux.cliente = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                entregado.Add(aux);
            }
            ViewBag.mandados_entregado = entregado;
            return View();
        }
    }
}