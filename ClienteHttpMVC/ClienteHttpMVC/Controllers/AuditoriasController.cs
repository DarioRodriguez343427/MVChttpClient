using ClienteHttpMVC.Clases_auxiliares;
using ClienteHttpMVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentacion.Controllers;

namespace ClienteHttpMVC.Controllers
{
    public class AuditoriasController : BaseController
    {
        private string URLApiAuditorias { get; set; }

        public AuditoriasController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiAuditorias = config.GetValue<string>("URLApiAuditorias");
            }

            if (env.IsProduction())
            {
                URLApiAuditorias = config.GetValue<string>("URLApiAuditoriasAZURE");
            }
        }

        public IActionResult ListarAuditorias(int? id)
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (ObtenerRol() != "Administrador")
            {
                ViewBag.Error = "Su rol no esta autorizado a realizar esta operacion";
                return View();
            }

            IEnumerable<LogDTO> lista = new List<LogDTO>();

            if (id == null) return View(lista);

            try
            {
                var token = HttpContext.Session.GetString("token");
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiAuditorias + "/" + id, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<List<LogDTO>>(body);
                }
                else
                {
                    ViewBag.mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "ocurrio un error inesperado, intente nuevamente";
            }
            return View(lista);
        }
    }
}
