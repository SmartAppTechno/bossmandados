using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class CrearMandadoController : Controller
    {
        private RepartidoresEntities db_repartidores = new RepartidoresEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();
        private ServiciosEntities db_servicios = new ServiciosEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosRutasEntities db_rutas = new MandadosRutasEntities();
        private MandadosEstadosEntities db_estados = new MandadosEstadosEntities();
        private ClientesEntities db_clientes = new ClientesEntities();

        public struct Repartidor
        {
            public int id;
            public string nombre;
        }

        public struct Servicio
        {
            public int id;
            public string nombre;
        }

        public struct Estado
        {
            public int id;
            public string nombre;
        }

        public struct Cliente
        {
            public int id;
            public string nombre;
        }

        // GET: CrearMandado
        public ActionResult Index()
        {
            if (Session["nombre_usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Repartidores
            List<Repartidor> mandaderos = new List<Repartidor>();
            var repartidores_db = db_repartidores.manboss_repartidores.ToList();
            foreach (var repartidor in repartidores_db)
            {
                Repartidor aux = new Repartidor();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.id = repartidor.repartidor;
                mandaderos.Add(aux);
            }
            ViewBag.mandaderos = mandaderos;
            //Servicios
            List<Servicio> servicios = new List<Servicio>();
            var servicios_db = db_servicios.manboss_servicios.ToList();
            foreach (var servicio in servicios_db)
            {
                Servicio aux = new Servicio();
                aux.id = servicio.id;
                aux.nombre = servicio.nombre;
                servicios.Add(aux);
            }
            ViewBag.servicios = servicios;
            //Estados
            List<Estado> estados = new List<Estado>();
            var estados_db = db_estados.manboss_mandados_estados.ToList();
            foreach (var estado in estados_db)
            {
                Estado aux = new Estado();
                aux.id = estado.id;
                aux.nombre = estado.nombre;
                estados.Add(aux);
            }
            ViewBag.estados = estados;
            //Clientes
            List<Cliente> clientes = new List<Cliente>();
            var clientes_db = db_clientes.manboss_clientes.ToList();
            foreach (var cliente in clientes_db)
            {
                Cliente aux = new Cliente();
                aux.id = cliente.id;
                aux.nombre = cliente.nombre;
                clientes.Add(aux);
            }
            ViewBag.clientes = clientes;
            //Rutas
            ViewBag.rutas = Session["rutas_mandados"];
            return View();
        }

        [HttpPost]
        public ActionResult Agregar_Ruta(string calle,int numero,int servicio, float latitud, float longitud, string comentarios)
        {
            if (Session["nombre_usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Ruta aux = new Ruta();
            aux.id_servicio = servicio;
            aux.servicio = db_servicios.manboss_servicios.Where(x => x.id == servicio).Select(x => x.nombre).FirstOrDefault();
            aux.calle = calle;
            aux.numero = numero;
            aux.latitud = latitud;
            aux.longitud = longitud;
            aux.comentarios = comentarios;
            List<Ruta> rutas = (List<Ruta>)Session["rutas_mandados"];
            rutas.Add(aux);
            Session["rutas_mandados"] = rutas;
            ViewBag.rutas = Session["rutas_mandados"];
            return Content("exito");
        }

        [HttpPost]
        public ActionResult Agregar_Mandado_Nuevo(string nombre, string correo, string telefono,int mandadero)
        {
            if (Session["nombre_usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Crear Cliente
            manboss_clientes nuevo_cliente = new manboss_clientes();
            nuevo_cliente.nombre = nombre;
            nuevo_cliente.correo = correo;
            nuevo_cliente.telefono = telefono;
            db_clientes.manboss_clientes.Add(nuevo_cliente);
            db_clientes.SaveChanges();
            int cliente_id = nuevo_cliente.id;
            //Crear Mandado
            manboss_mandados nuevo_mandado = new manboss_mandados();
            if (mandadero > 0)
            {
                nuevo_mandado.repartidor = mandadero;
                nuevo_mandado.estado = 2;
            }
            else
            {
                nuevo_mandado.estado = 1;
            }
            nuevo_mandado.cliente = cliente_id;
            nuevo_mandado.fecha = DateTime.Now;
            nuevo_mandado.tipo_pago = 0;
            nuevo_mandado.cuenta_pendiente = 0;
            db_mandados.manboss_mandados.Add(nuevo_mandado);
            db_mandados.SaveChanges();
            int mandado_id = nuevo_mandado.id;
            //Crear Rutas del Mandado
            List<Ruta> rutas = (List<Ruta>)Session["rutas_mandados"];
            foreach (var ruta in rutas)
            {
                manboss_mandados_rutas nueva_ruta = new manboss_mandados_rutas();
                nueva_ruta.mandado = mandado_id;
                nueva_ruta.servicio = ruta.id_servicio;
                nueva_ruta.calle = ruta.calle;
                nueva_ruta.numero = ruta.numero;
                nueva_ruta.latitud = ruta.latitud;
                nueva_ruta.longitud = ruta.longitud;
                nueva_ruta.comentarios = ruta.comentarios;
                nueva_ruta.terminado = 0;
                db_rutas.manboss_mandados_rutas.Add(nueva_ruta);
                db_rutas.SaveChanges();
            }
            return Content("exito");
        }

        [HttpPost]
        public ActionResult Agregar_Mandado_Existente(int cliente, int mandadero)
        {
            if (Session["nombre_usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Crear Mandado
            manboss_mandados nuevo_mandado = new manboss_mandados();
            if (mandadero > 0)
            {
                nuevo_mandado.repartidor = mandadero;
                nuevo_mandado.estado = 2;
            }
            else
            {
                nuevo_mandado.estado = 1;
            }
            nuevo_mandado.cliente = cliente;
            nuevo_mandado.fecha = DateTime.Now;
            nuevo_mandado.tipo_pago = 0;
            nuevo_mandado.cuenta_pendiente = 0;
            db_mandados.manboss_mandados.Add(nuevo_mandado);
            db_mandados.SaveChanges();
            int mandado_id = nuevo_mandado.id;
            //Crear Rutas del Mandado
            List<Ruta> rutas = (List<Ruta>)Session["rutas_mandados"];
            foreach (var ruta in rutas)
            {
                manboss_mandados_rutas nueva_ruta = new manboss_mandados_rutas();
                nueva_ruta.mandado = mandado_id;
                nueva_ruta.servicio = ruta.id_servicio;
                nueva_ruta.calle = ruta.calle;
                nueva_ruta.numero = ruta.numero;
                nueva_ruta.latitud = ruta.latitud;
                nueva_ruta.longitud = ruta.longitud;
                nueva_ruta.comentarios = ruta.comentarios;
                nueva_ruta.terminado = 0;
                db_rutas.manboss_mandados_rutas.Add(nueva_ruta);
                db_rutas.SaveChanges();
            }
            return Content("exito");
        }
    }
}