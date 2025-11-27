using ClienteHttpMVC.Clases_auxiliares;
using ClienteHttpMVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentacion.Controllers;

namespace ClienteHttpMVC.Controllers
{
    public class UsuariosController : BaseController
    {
        private string URLApiUsuarios { get; set; }
        public UsuariosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiUsuarios = config.GetValue<string>("URLApiUsuarios");

            }

            if (env.IsProduction())
            {
                URLApiUsuarios = config.GetValue<string>("URLApiUsuariosAZURE");
            }
        }

        //GET: usuariosController
        public IActionResult Index()
        {
            if (ObtenerRol() == null) return View("NoAutorizado");
            return View();
        }


        //GET: usuariosController/ListarUsuarios
        public IActionResult ListarUsuarios()
        {
            if(ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (ObtenerRol() != "Administrador")
            {
                ViewBag.Error = "Su rol no permite realizar esta operación";
                return View();
            }

            IEnumerable<ListadoUsuariosDTO> lista = null;
            try
            {
                string token = HttpContext.Session.GetString("token");
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios + "/listarUsuarios", "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<IEnumerable<ListadoUsuariosDTO>>(body);
                }
                else
                {
                    ViewBag.Mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View(lista);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (ObtenerRol() != "Administrador")
            {
                ViewBag.Error = "Su rol no permite realizar esta operación";
                return View();
            }

            ListadoUsuariosDTO dto = null;

            try
            {
                string token = HttpContext.Session.GetString("token");
                var resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios + "/" + id, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resultado);

                if (resultado.IsSuccessStatusCode)
                {
                    dto = JsonConvert.DeserializeObject<ListadoUsuariosDTO>(body);
                }
                else
                {
                    ViewBag.Mensaje = body;
                }
            }
            catch(Exception)
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(ListadoUsuariosDTO dto)
        {

            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (ObtenerRol() != "Administrador")
            {
                ViewBag.Error = "Su rol no permite realizar esta operación";
                return View();
            }

            try
            {
                string token = HttpContext.Session.GetString("token");
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios + "/reinicioPassword/" + dto.Id , "PUT", dto, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ListadoUsuariosDTO dtoUsuario = JsonConvert.DeserializeObject<ListadoUsuariosDTO>(body);
                    ViewBag.Mensaje = "Contraseña reseteada, la nueva contraseña es: " + dtoUsuario.Contrasena;

                    var resultado2 = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios + "/" + dto.Id, "GET", null, token);
                    string body2 = AuxiliarClienteHttp.ObtenerBody(resultado2);
                    ListadoUsuariosDTO dto2 = JsonConvert.DeserializeObject<ListadoUsuariosDTO>(body2);
                    return View(dto2);
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + body;
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }

            return View();
        }
    }
}
