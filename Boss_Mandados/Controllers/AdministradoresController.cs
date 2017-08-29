using System.Linq;
using System.Net;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Specialized;

namespace Boss_Mandados
{
    public class AdministradoresController : Controller
    {
        private UsuariosEntities db = new UsuariosEntities();

        // GET: Administradores
        public ActionResult Index()
        {
            return View(db.manboss_usuarios.Where(x=> x.rol == 1).ToList());
        }

        // GET: Administradores/Details/5
        public ActionResult Details(int? id)
        {
            manboss_usuarios manboss_model = db.manboss_usuarios.Find(id);
            MenuEntities db_menu = new MenuEntities();
            ViewBag.menus = db_menu.manboss_permisos_menu.ToList();
            PermisosEntities db_permisos = new PermisosEntities();
            int usuario = (int)id;
            ViewBag.permisos = db_permisos.manboss_permisos.Where(x => x.usuario == usuario && x.estado == 1).Select(x => x.menu).ToList();
            return View(manboss_model);
        }

        // GET: Administradores/Create
        public ActionResult Create()
        {
            MenuEntities db_menu = new MenuEntities();
            ViewBag.menus = db_menu.manboss_permisos_menu.ToList();
            return View();
        }

        // POST: Administradores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,correo,usuario,contrasenia")] manboss_usuarios usuario_form)
        {
            if (db.manboss_usuarios.Any(x => x.usuario == usuario_form.usuario))
            {
                ViewBag.Message = "El usuario ya existe";
                ViewBag.nombre = usuario_form.nombre;
                ViewBag.correo = usuario_form.correo;
                ViewBag.contrasenia = usuario_form.contrasenia;
                MenuEntities db_menu = new MenuEntities();
                ViewBag.menus = db_menu.manboss_permisos_menu.ToList();
                return View();
            }
            else
            {
                string contrasenia_form = usuario_form.contrasenia;
                string hash = random_string(12);
                usuario_form.contrasenia = encrypt_pass(contrasenia_form + hash).ToLower();
                usuario_form.hash = hash;
                usuario_form.rol = 1;
                db.manboss_usuarios.Add(usuario_form);
                db.SaveChanges();
                //Agregar permisos
                NameValueCollection permisos_form = Request.Form;
                string permisos_str = permisos_form.AllKeys.ToDictionary(x => x, x => permisos_form[x]).Values.ToList().Last();
                string[] arr = permisos_str.Split(',');
                int[] permisos = Array.ConvertAll(arr, int.Parse);
                PermisosEntities db_permisos = new PermisosEntities();
                for(int i=1;i<=9;i++)
                {
                    manboss_permisos permiso = new manboss_permisos();
                    if (permisos.Contains(i))
                    {
                        permiso.usuario = usuario_form.id;
                        permiso.menu = i;
                        permiso.estado = 1;
                        db_permisos.manboss_permisos.Add(permiso);
                        db_permisos.SaveChanges();
                    }
                    else
                    {
                        permiso.usuario = usuario_form.id;
                        permiso.menu = i;
                        permiso.estado = 0;
                        db_permisos.manboss_permisos.Add(permiso);
                        db_permisos.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
        }

        // GET: Administradores/Edit/5
        public ActionResult Edit(int? id)
        {
            manboss_usuarios manboss_model = db.manboss_usuarios.Find(id);
            MenuEntities db_menu = new MenuEntities();
            ViewBag.menus = db_menu.manboss_permisos_menu.ToList();
            PermisosEntities db_permisos = new PermisosEntities();
            int usuario = (int) id;
            ViewBag.permisos = db_permisos.manboss_permisos.Where(x=> x.usuario == usuario && x.estado == 1).Select(x=> x.menu).ToList();
            return View(manboss_model);
        }

        // POST: Administradores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,correo,usuario,contrasenia")] manboss_usuarios usuario_form)
        {
            var usuario_actual = db.manboss_usuarios.FirstOrDefault(x => x.id == usuario_form.id);
            usuario_actual.nombre = usuario_form.nombre;
            usuario_actual.correo = usuario_form.correo;
            usuario_actual.usuario = usuario_form.usuario;
            if (!string.IsNullOrEmpty(usuario_form.contrasenia))
            {
                string contrasenia_form = usuario_form.contrasenia;
                string hash = random_string(12);
                usuario_actual.contrasenia = encrypt_pass(contrasenia_form + hash).ToLower();
                usuario_actual.hash = hash;
            }
            db.SaveChanges();
            //Agregar permisos
            NameValueCollection permisos_form = Request.Form;
            string permisos_str = permisos_form.AllKeys.ToDictionary(x => x, x => permisos_form[x]).Values.ToList().Last();
            string[] arr = permisos_str.Split(',');
            int[] permisos = Array.ConvertAll(arr, int.Parse);
            PermisosEntities db_permisos = new PermisosEntities();
            for (int i = 1; i <= 9; i++)
            {
                var permiso_actual = db_permisos.manboss_permisos.FirstOrDefault(x => x.usuario == usuario_form.id && x.menu == i);
                if (permisos.Contains(i))
                {
                    permiso_actual.estado = 1;
                    db_permisos.SaveChanges();
                }
                else
                {
                    permiso_actual.estado = 0;
                    db_permisos.SaveChanges();
                }
            }
            return RedirectToAction("Index");
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

        // GET: Administradores/Delete/5
        public ActionResult Delete(int? id)
        {
            manboss_usuarios manboss_usuarios = db.manboss_usuarios.Find(id);
            return View(manboss_usuarios);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manboss_usuarios manboss_usuarios = db.manboss_usuarios.Find(id);
            db.manboss_usuarios.Remove(manboss_usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}