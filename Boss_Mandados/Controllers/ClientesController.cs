using System.Linq;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class ClientesController : Controller
    {
        private ClientesEntities db = new ClientesEntities();
        private DireccionesEntities db_direcciones = new DireccionesEntities();
        private MandadosEntities db_mandados = new MandadosEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.manboss_clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            manboss_clientes manboss_clientes = db.manboss_clientes.Find(id);
            return View(manboss_clientes);
        }

        public ActionResult Direcciones(int? id)
        {
            ViewBag.direcciones = db_direcciones.manboss_clientes_direcciones.Where(x => x.cliente == id).ToList();
            return View();
        }

        public ActionResult Mandados(int? id)
        {
            ViewBag.direcciones = db_direcciones.manboss_clientes_direcciones.Where(x => x.cliente == id).ToList();
            return View();
        }
    }
}
