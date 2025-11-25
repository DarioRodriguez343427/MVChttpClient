using ClienteHttpMVC.Clases_auxiliares;
using ClienteHttpMVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentacion.Controllers;
using System.Security.Cryptography.X509Certificates;

namespace ClienteHttpMVC.Controllers
{
    public class LoginController : BaseController
    {
        private string URLApiUsuarios { get; set; }
        public LoginController(IConfiguration config)
        {
            URLApiUsuarios = config.GetValue<string>("URLApiUsuarios");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO dto)
        {
            try
            {
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios, "POST", new {dto.Email, dto.Contrasena });
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    UsuarioDTO dtoUsuario = JsonConvert.DeserializeObject<UsuarioDTO>(body);

                    HttpContext.Session.SetString("id", dtoUsuario.Id.ToString());
                    HttpContext.Session.SetString("rol", dtoUsuario.Rol);
                    HttpContext.Session.SetString("token", dtoUsuario.token);

                    return RedirectToAction("Index", "Usuarios");
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

            return View();
        }
    }
}
