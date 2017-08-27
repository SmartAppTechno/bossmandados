using System.Linq;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Data.Entity.SqlServer;

namespace Boss_Mandados.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        public ActionResult Index()
        {
            ClientesEntities db_clientes = new ClientesEntities();
            ServiciosEntities db_servicios = new ServiciosEntities();
            RepartidoresEntities db_repartidores = new RepartidoresEntities();
            UsuariosEntities db_administradores = new UsuariosEntities();
            MandadosEntities db_mandados = new MandadosEntities();
            ViewBag.clientes = db_clientes.manboss_clientes.Count();
            ViewBag.servicios = db_servicios.manboss_servicios.Count();
            ViewBag.repartidores = db_repartidores.manboss_repartidores.Count();
            ViewBag.administradores = db_administradores.manboss_usuarios.Count();
            ViewBag.mandados_espera = db_mandados.manboss_mandados.Count(x => x.estado == 1);
            ViewBag.mandados_asignado = db_mandados.manboss_mandados.Count(x => x.estado == 2);
            ViewBag.mandados_proceso = db_mandados.manboss_mandados.Count(x => x.estado == 3);
            ViewBag.mandados_entregado = db_mandados.manboss_mandados.Count(x => x.estado == 4);
            var todaysDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            ViewBag.mandados_hoy = db_mandados.manboss_mandados.Count(x => x.fecha == todaysDate);
            ViewBag.mandados_semana = db_mandados.manboss_mandados.Count(x => SqlFunctions.DatePart("ISO_WEEK", x.fecha) == SqlFunctions.DatePart("ISO_WEEK", DateTime.Now));
            ViewBag.mandados_mes = db_mandados.manboss_mandados.Count(x => x.fecha.Month == DateTime.Now.Month);
            ViewBag.mandados_anio = db_mandados.manboss_mandados.Count(x => x.fecha.Year == DateTime.Now.Year);
            var mandados_meses = db_mandados.manboss_mandados.Where(x=> x.fecha.Year == DateTime.Now.Year).GroupBy(x => new { Month = x.fecha.Month }).Select(x=> new { Month = x.Key.Month }).ToList();
            var mandados_cantidad = db_mandados.manboss_mandados.Where(x => x.fecha.Year == DateTime.Now.Year).GroupBy(x => new { Month = x.fecha.Month }).Select(x => new { Total = x.Count() }).ToList();
            List<string> meses = new List<string>();
            foreach(var mandado in mandados_meses)
            {
                string m = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mandado.Month);
                m = char.ToUpper(m[0]) + m.Substring(1);
                meses.Add(m);
            }
            ViewBag.meses = meses.ToArray();
            List<int> mandados = new List<int>();
            foreach (var mandado in mandados_cantidad)
            {
                mandados.Add(mandado.Total);
            }
            ViewBag.mandados = mandados.ToArray();
            return View();
        }
    }
}