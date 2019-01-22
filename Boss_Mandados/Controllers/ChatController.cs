using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;
using System.Globalization;

namespace Boss_Mandados.Controllers
{
    public class ChatController : Controller
    {
        public struct Conversacion
        {
            public int id;
            public string fecha;
            public string cliente;
            public string repartidor;
        }

        public struct Mensaje
        {
            public string mensaje;
            public int tipo;
        }

        private ChatEntities db_chat = new ChatEntities();
        private RepartidoresEntities db_repartidores = new RepartidoresEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private ClientesEntities db_clientes = new ClientesEntities();
        private ChatMensajeEntities db_mensajes = new ChatMensajeEntities();

        // GET: Chat
        public ActionResult Index()
        {
            List<Conversacion> conversaciones = new List<Conversacion>();
            var conversacion_db = db_chat.manboss_chat.ToList();
            foreach (var chat in conversacion_db)
            {
                Conversacion aux = new Conversacion();
                aux.id = chat.id;
                aux.fecha = db_mandados.manboss_mandados.Where(x=> x.id == chat.mandado).Select(x=> x.fecha).FirstOrDefault().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                int mandado_cliente = db_mandados.manboss_mandados.Where(x => x.id == chat.mandado).Select(x => x.cliente).FirstOrDefault();
                aux.cliente = db_clientes.manboss_clientes.Where(x => x.id == mandado_cliente).Select(x => x.nombre).FirstOrDefault();
                int mandado_repartidor = db_repartidores.manboss_repartidores.Where(x => x.id == chat.repartidor).Select(x => x.repartidor).FirstOrDefault();
                aux.repartidor = db_usuarios.manboss_usuarios.Where(x => x.id == mandado_repartidor).Select(x => x.nombre).FirstOrDefault();
                conversaciones.Add(aux);
            }
            ViewBag.conversaciones = conversaciones;
            return View();
        }

        // GET: Chat/Details/5
        public ActionResult Details(int? id)
        {
            List<Mensaje> mensajes = new List<Mensaje>();
            var mensajes_db = db_mensajes.manboss_chat_mensajes.Where(x => x.chat == id).ToList();
            foreach (var item in mensajes_db)
            {
                Mensaje aux = new Mensaje();
                //aux.tipo = item.tipo;
                aux.mensaje = item.mensaje;
                mensajes.Add(aux);
            }
            ViewBag.mensajes = mensajes;
            return View();
        }
    }
}
