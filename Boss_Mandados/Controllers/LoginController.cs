using System.Linq;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Security.Cryptography;
using System.Text;

namespace Boss_Mandados.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Iniciar_Sesion(manboss_usuarios Login)
        {
            UsuariosEntities db_usuarios = new UsuariosEntities();
            var usuario_info = db_usuarios.manboss_usuarios.Where(x => x.usuario == Login.usuario).FirstOrDefault();
            if (usuario_info == null)
            {
                ViewBag.Message = "El usuario no existe.";
                return View("Index", Login);
            }
            else
            {
                string contrasenia_final = usuario_info.contrasenia;
                string contrasenia_form = Login.contrasenia;
                string hash = usuario_info.hash;
                string contrasenia_res = encrypt_pass(contrasenia_form + hash).ToLower();
                if (contrasenia_res.Equals(contrasenia_final))
                {
                    RolUsuarioEntities db_roles = new RolUsuarioEntities();
                    var rol_info = db_roles.manboss_rol_usuario.Where(x=> x.id == usuario_info.rol).FirstOrDefault();
                    PermisosEntities db_permisos = new PermisosEntities();
                    Session["permisos"] = db_permisos.manboss_permisos.Where(x => x.usuario == usuario_info.id && x.estado == 1).Select(x => x.menu).ToList();
                    Session["id_usuario"] = usuario_info.id;
                    Session["nombre_usuario"] = usuario_info.nombre;
                    Session["rol_usuario"] = usuario_info.rol;
                    Session["rolnombre_usuario"] = rol_info.nombre;
                    return RedirectToAction("Index", "Panel");
                }
                else
                {
                    ViewBag.Message = "La contraseña es incorrecta.";
                    return View("Index", Login);
                }
            }
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
        public ActionResult Cerrar_Sesion()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}