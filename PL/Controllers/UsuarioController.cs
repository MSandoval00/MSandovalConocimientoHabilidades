using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario=new ML.Usuario();
            usuario.Usuarios = new List<object>();
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("usuario");
                responseTask.Wait();
                var resultService=responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultUsuario in readTask.Result.Objects)
                    {
                        ML.Usuario resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultUsuario.ToString());
                        usuario.Usuarios.Add(resultList);
                    }
                   
                }
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios=new List<object>();

            if (IdUsuario!=null)//Update
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTask = client.GetAsync("usuario/" + IdUsuario);
                    responseTask.Wait();
                    var resultService = responseTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        
                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            usuario = resultUsuario;
                        
                    }
                }
            }
            else//Add
            {

            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0)//Add
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]+"usuario");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress, usuario);
                    postTask.Wait();
                    var result=postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se registro correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo registrar el usuario";
                    }
                }
            }
            else//Update
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var postTask=client.PutAsJsonAsync("usuario/"+usuario.IdUsuario, usuario);
                    postTask.Wait();
                    var result= postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar correctamente";
                    }
                }

            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var deleteTask = client.DeleteAsync("usuario/" + IdUsuario);
                deleteTask.Wait();
                var result=deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamente el usuario";
                }
                else
                {
                    ViewBag.Mensaje = "No se elimino correctamente el usuario";
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            ML.Result result = BL.Usuario.UsuarioGetByEmail(email);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Session["IdUsuario"] = usuario.IdUsuario;
                Session["Nombre"] = usuario.Nombre;
                Session["ApellidoPaterno"] = usuario.ApellidoPaterno;
                Session["ApellidoMaterno"] = usuario.ApellidoMaterno;
                Session["NombreRol"] = usuario.Rol.Nombre;


                if (password==usuario.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Login = true;
                    ViewBag.Mensaje = "La contraseña es incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Login = true;
                ViewBag.Mensaje = "El email es incorrecto";
                return PartialView("Modal");
            }
        }

    }
}