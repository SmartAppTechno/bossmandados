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
        private ClientesEntities db_clientes = new ClientesEntities();
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

        public struct Mandadero
        {
            public string nombre;
            public double latitud;
            public double longitud;
        }

        public struct Repartidor
        {
            public int id;
            public string nombre;
        }

        public struct Lugar
        {
            public int mandado;
            public double latitud;
            public double longitud;
            public string comentarios;
        }

        public struct Ruta
        {
            public int mandado;
            public List<Lugar> puntos;
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                entregado.Add(aux);
            }
            ViewBag.mandados_entregado = entregado;
            //Mandaderos
            List<Mandadero> mandaderos = new List<Mandadero>();
            var mandaderos_db = db_repartidores.manboss_repartidores.ToList();
            foreach (var mandadero in mandaderos_db)
            {
                Mandadero aux = new Mandadero();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == mandadero.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.latitud = mandadero.latitud;
                aux.longitud = mandadero.longitud;
                mandaderos.Add(aux);
            }
            ViewBag.mandaderos = mandaderos;
            //Repartidores
            List<Repartidor> repartidores = new List<Repartidor>();
            var mandados_proceso = db_mandados.manboss_mandados.Where(x=> x.estado == 3).Select(x=> x.repartidor).Distinct().ToList();
            foreach (var repartidor in mandados_proceso)
            {
                Repartidor aux = new Repartidor();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.id = (int) repartidor;
                repartidores.Add(aux);
            }
            ViewBag.repartidores = repartidores;
            //Rutas de los Mandados
            List<Ruta> rutas = new List<Ruta>();
            var rutas_db = db_rutas.manboss_mandados_rutas.ToList();
            var mandados_id = db_rutas.manboss_mandados_rutas.Select(x => x.mandado).Distinct().ToList();
            foreach(var mandado in mandados_id)
            {
                Ruta aux = new Ruta();
                int estado_actual = db_mandados.manboss_mandados.Where(x => x.id == mandado).Select(x => x.estado).FirstOrDefault();
                if(estado_actual == 3)
                {
                    aux.mandado = mandado;
                    aux.puntos = new List<Lugar>();
                    foreach (var punto in rutas_db)
                    {
                        if (punto.mandado == aux.mandado)
                        {
                            Lugar temp = new Lugar();
                            temp.mandado = punto.mandado;
                            temp.latitud = punto.latitud;
                            temp.longitud = punto.longitud;
                            temp.comentarios = punto.comentarios;
                            aux.puntos.Add(temp);
                        }
                    }
                    rutas.Add(aux);
                }
            }
            ViewBag.rutas = rutas;
            return View();
        }

        [HttpPost]
        public ActionResult Mandado_Ruta(int repartidor_id)
        {
            //En Espera
            List<Mandado> espera = new List<Mandado>();
            var espera_db = db_mandados.manboss_mandados.Where(x => x.estado == 1).ToList();
            foreach (var mandado in espera_db)
            {
                Mandado aux = new Mandado();
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
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
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado.cliente).Select(x => x.nombre).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.total = mandado.total;
                aux.fecha = mandado.fecha.ToString("dd/MM/yyyy");
                entregado.Add(aux);
            }
            ViewBag.mandados_entregado = entregado;
            //Repartidores
            List<Repartidor> repartidores = new List<Repartidor>();
            var mandados_proceso = db_mandados.manboss_mandados.Where(x => x.estado == 3).Select(x => x.repartidor).Distinct().ToList();
            foreach (var repartidor in mandados_proceso)
            {
                Repartidor aux = new Repartidor();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.id = (int)repartidor;
                repartidores.Add(aux);
            }
            ViewBag.repartidores = repartidores;
            //Mandaderos
            List<Mandadero> mandaderos = new List<Mandadero>();
            Mandadero aux_mandadero = new Mandadero();
            aux_mandadero.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor_id).Select(x => x.nombre).FirstOrDefault();
            aux_mandadero.latitud = db_repartidores.manboss_repartidores.Where(x=> x.repartidor == repartidor_id).Select(x=> x.latitud).FirstOrDefault();
            aux_mandadero.longitud = db_repartidores.manboss_repartidores.Where(x => x.repartidor == repartidor_id).Select(x => x.longitud).FirstOrDefault();
            mandaderos.Add(aux_mandadero);
            ViewBag.mandaderos = mandaderos;
            //Rutas de los Mandados
            List<Ruta> rutas = new List<Ruta>();
            var rutas_db = db_rutas.manboss_mandados_rutas.ToList();
            var mandados_id = db_rutas.manboss_mandados_rutas.Select(x => x.mandado).Distinct().ToList();
            foreach (var mandado in mandados_id)
            {
                Ruta aux = new Ruta();
                int estado_actual = db_mandados.manboss_mandados.Where(x => x.id == mandado).Select(x => x.estado).FirstOrDefault();
                if (estado_actual == 3)
                {
                    int repartidor_mandado = (int) db_mandados.manboss_mandados.Where(x => x.id == mandado).Select(x => x.repartidor).FirstOrDefault();
                    if(repartidor_mandado == repartidor_id)
                    {
                        aux.mandado = mandado;
                        aux.puntos = new List<Lugar>();
                        foreach (var punto in rutas_db)
                        {
                            if (punto.mandado == aux.mandado)
                            {
                                Lugar temp = new Lugar();
                                temp.mandado = punto.mandado;
                                temp.latitud = punto.latitud;
                                temp.longitud = punto.longitud;
                                temp.comentarios = punto.comentarios;
                                aux.puntos.Add(temp);
                            }
                        }
                        rutas.Add(aux);
                    }
                }
            }
            ViewBag.rutas = rutas;
            return View();
        }
    }
}