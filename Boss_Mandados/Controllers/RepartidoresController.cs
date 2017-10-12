using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace Boss_Mandados.Controllers
{
    public class RepartidoresController : Controller
    {
        public struct Repartidor
        {
            public manboss_repartidores repartidor;
            public manboss_usuarios usuario;
        }
        private RepartidoresEntities db = new RepartidoresEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosEstadosEntities db_mandados_estados = new MandadosEstadosEntities();
        private MandadosRutasEntities db_mandados_rutas = new MandadosRutasEntities();
        private ServiciosEntities db_servicios = new ServiciosEntities();
        private ClientesEntities db_clientes = new ClientesEntities();
        private ComisionesEntities db_comisiones = new ComisionesEntities();
        private RepartidoresCalificacionesEntities db_calificaciones = new RepartidoresCalificacionesEntities();

        // GET: Repartidores
        public ActionResult Index()
        {
            List<Repartidor> repartidores = new List<Repartidor>();
            var rep_db = db.manboss_repartidores.ToList();
            foreach(var repartidor in rep_db)
            {
                manboss_usuarios user = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).FirstOrDefault();
                double total = db_calificaciones.manboss_repartidores_calificaciones.Where(x => x.repartidor == repartidor.id).Sum(x=> x.calificacion);
                int pedidos = db_calificaciones.manboss_repartidores_calificaciones.Where(x => x.repartidor == repartidor.id).Count();
                repartidor.rating = total / pedidos;
                Repartidor temporal = new Repartidor();
                temporal.repartidor = repartidor;
                temporal.usuario = user;
                repartidores.Add(temporal);
            }
            ViewBag.repartidores = repartidores;
            return View();
        }

        // GET: Repartidores/Details/5
        public ActionResult Details(int? id)
        {
            manboss_repartidores repartidor = db.manboss_repartidores.Find(id);
            Repartidor temporal = new Repartidor();
            manboss_usuarios user = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).FirstOrDefault();
            temporal.usuario = user;
            temporal.repartidor = repartidor;
            ViewBag.item = temporal;
            return View();
        }

        // GET: Repartidores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repartidores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "efectivo")] manboss_repartidores repartidor_form)
        {
            NameValueCollection usuario_form = Request.Form;
            string usuario = usuario_form["usuario"];
            if (db_usuarios.manboss_usuarios.Any(x => x.usuario == usuario))
            {
                ViewBag.Message = "El usuario ya existe";
                ViewBag.efectivo = repartidor_form.efectivo;
                ViewBag.nombre = usuario_form["nombre"];
                ViewBag.correo = usuario_form["correo"];
                ViewBag.contrasenia = usuario_form["contrasenia"];
                return View();
            }
            else
            {
                //Crear el usuario
                manboss_usuarios nuevo_usuario = new manboss_usuarios();
                string contrasenia_form = usuario_form["contrasenia"];
                string hash = random_string(12);
                nuevo_usuario.nombre = usuario_form["nombre"];
                nuevo_usuario.correo = usuario_form["correo"];
                nuevo_usuario.usuario = usuario_form["usuario"];
                nuevo_usuario.contrasenia = encrypt_pass(contrasenia_form + hash).ToLower();
                nuevo_usuario.hash = hash;
                nuevo_usuario.rol = 3;
                db_usuarios.manboss_usuarios.Add(nuevo_usuario);
                db_usuarios.SaveChanges();
                //Crear el repartidor
                repartidor_form.repartidor = nuevo_usuario.id;
                repartidor_form.latitud = 0;
                repartidor_form.longitud = 0;
                repartidor_form.rating = 5;
                db.manboss_repartidores.Add(repartidor_form);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Repartidores/Edit/5
        public ActionResult Edit(int? id)
        {
            manboss_repartidores repartidor = db.manboss_repartidores.Find(id);
            manboss_usuarios user = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).FirstOrDefault();
            ViewBag.usuario = user;
            return View(repartidor);
        }

        // POST: Repartidores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,repartidor,efectivo")] manboss_repartidores repartidor_form)
        {
            //Guardar usuario
            NameValueCollection usuario_form = Request.Form;
            var usuario_actual = db_usuarios.manboss_usuarios.FirstOrDefault(x => x.id == repartidor_form.repartidor);
            usuario_actual.nombre = usuario_form["nombre"];
            usuario_actual.correo = usuario_form["correo"];
            usuario_actual.usuario = usuario_form["usuario"];
            if (!string.IsNullOrEmpty(usuario_form["contrasenia"]))
            {
                string contrasenia_form = usuario_form["contrasenia"];
                string hash = random_string(12);
                usuario_actual.contrasenia = encrypt_pass(contrasenia_form + hash).ToLower();
                usuario_actual.hash = hash;
            }
            db_usuarios.SaveChanges();
            var repartidor_actual = db.manboss_repartidores.FirstOrDefault(x => x.id == repartidor_form.id);
            repartidor_actual.efectivo = repartidor_form.efectivo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public struct Mandado_object
        {
            public int id;
            public string estado;
            public string fecha;
            public string cliente;
            public string total;
            public string tiempo;
            public string cuenta;
        }

        public struct Mandado_detalle
        {
            public string servicio;
            public string comentarios;
            public float latitud;
            public float longitud;
        }

        public ActionResult Mandados(int? id)
        {
            List<Mandado_object> mandados = new List<Mandado_object>();
            var mandados_db = db_mandados.manboss_mandados.Where(x => x.repartidor == id).ToList();
            foreach (var mandado in mandados_db)
            {
                Mandado_object aux = new Mandado_object();
                aux.id = mandado.id;
                aux.estado = db_mandados_estados.manboss_mandados_estados.Where(x => x.id == mandado.estado).Select(x => x.nombre).FirstOrDefault();
                aux.fecha = mandado.fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.total = String.Format("{0:C}", mandado.total);
                aux.tiempo = mandado.tiempo_total.ToString();
                if(mandado.cuenta_pendiente == 0) { aux.cuenta = "Pagada"; } else if(mandado.cuenta_pendiente == 1) { aux.cuenta = "Pendiente"; }
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
                aux.latitud = (float)mandado.latitud;
                aux.longitud = (float)mandado.longitud;
                mandados.Add(aux);
            }
            ViewBag.mandado = mandados;
            ViewBag.mandado_id = db_mandados.manboss_mandados.Where(x => x.id == id).Select(x => x.cliente).FirstOrDefault();
            return View();
        }

        public struct Comision
        {
            public string fecha;
            public string cliente;
            public string total;
            public string comision;
        }

        public ActionResult Comisiones(int? id)
        {
            List<Comision> comisiones = new List<Comision>();
            var comisiones_db = db_comisiones.manboss_comisiones.Where(x => x.repartidor == id).ToList();
            foreach (var com in comisiones_db)
            {
                Comision aux = new Comision();
                aux.fecha = db_mandados.manboss_mandados.Where(x => x.id == com.mandado).Select(x => x.fecha).FirstOrDefault().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                int mandado_cliente = db_mandados.manboss_mandados.Where(x => x.id == com.mandado).Select(x => x.cliente).FirstOrDefault();
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado_cliente).Select(x => x.nombre).FirstOrDefault();
                aux.total = String.Format("{0:C}", db_mandados.manboss_mandados.Where(x => x.id == com.mandado).Select(x => x.total).FirstOrDefault());
                aux.comision = String.Format("{0:C}", com.comision);
                comisiones.Add(aux);
            }
            ViewBag.comisiones = comisiones;
            return View();
        }

        public static string random_string(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string encrypt_pass(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
