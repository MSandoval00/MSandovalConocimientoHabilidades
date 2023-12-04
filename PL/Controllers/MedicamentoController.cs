using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento=new ML.Medicamento();
            medicamento.Medicamentos = new List<object>();

            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("medicamento");
                responseTask.Wait();
                var resultService=responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultMedicamento in readTask.Result.Objects)
                    {
                        ML.Medicamento medicamentoLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(resultMedicamento.ToString());
                        medicamento.Medicamentos.Add(medicamentoLista);
                    }
                }
            }
            return View(medicamento);
        }
        [HttpGet]
        public ActionResult Form(int? IdMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = new List<object>();
            if (IdMedicamento!=null)//Update 
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTask = client.GetAsync("medicamento/" + IdMedicamento);
                    responseTask.Wait();
                    var resultService = responseTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Medicamento medicamentoLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(readTask.Result.Object.ToString());
                        medicamento = medicamentoLista;
                    }
                }
            }
            else//Add
            {

            }
            return View(medicamento);
        }
        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            if (medicamento.IdMedicamento==0)//Add
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"] + "medicamento");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress, medicamento);
                    postTask.Wait();
                    var result=postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se agrego correctamente el medicamento";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar el medicamento";
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var putTask = client.PutAsJsonAsync("medicamento/"+ medicamento.IdMedicamento, medicamento);
                    putTask.Wait();
                    var result=putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo correctamente el medicamento";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar el medicamento";
                    }
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdMedicamento)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var deleteTask = client.DeleteAsync("medicamento/" + IdMedicamento);
                deleteTask.Wait();
                var result=deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamene el medicamento";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo eliminar el medicamento";
                }
            }
            return PartialView("Modal");
        }
        
        public ActionResult AddMedicamento(int IdMedicamento)
        {
            bool existe=false;
            ML.Venta listMedicamentos = new ML.Venta();
            listMedicamentos.ListaMedicamentos = new List<object>();
            ML.Result result = BL.Medicamento.GetById(IdMedicamento);
            //Session["ListaMedicamentos"]= listMedicamentos.ListaMedicamentos;
            var Lista = Session["ListaMedicos"];
            if (Lista==null)//session
            {
                if (result.Correct)
                {
                    ML.Medicamento medicamento = (ML.Medicamento)result.Object;
                    medicamento.Piezas = 1;
                    listMedicamentos.ListaMedicamentos.Add(medicamento);
                    string json= Newtonsoft.Json.JsonConvert.SerializeObject(listMedicamentos);
                    Lista = json;
                }
            }
            else
            {
                ML.Medicamento medicamento = (ML.Medicamento)result.Object;
                foreach (ML.Medicamento item in listMedicamentos.ListaMedicamentos)
                {
                    if (medicamento.IdMedicamento==item.IdMedicamento)
                    {
                        item.Piezas += 1;
                        existe = true;
                        break;
                    }
                    else
                    {
                        existe = false;
                    }
                    
                }
                if (existe==true)
                {

                }
                else
                {
                    medicamento.Piezas = 1;
                    listMedicamentos.ListaMedicamentos.Add(medicamento);

                }
            }
            return RedirectToAction("Farmacos");
        }
        [HttpGet]
        public ActionResult Farmacos()
        {
            return View();
        }
       
    }
}