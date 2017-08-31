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

        // GET: Repartidores
        public ActionResult Index()
        {
            List<Repartidor> repartidores = new List<Repartidor>();
            var rep_db = db.manboss_repartidores.ToList();
            foreach(var repartidor in rep_db)
            {
                manboss_usuarios user = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).FirstOrDefault();
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

        public ActionResult Mandados(int? id)
        {
            
            return View();
        }

        public ActionResult Comisiones(int? id)
        {

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
