
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("medicamento");
                responseTask.Wait();
                var resultService = responseTask.Result;
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
            if (IdMedicamento != null)//Update 
            {
                using (var client = new HttpClient())
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
            if (medicamento.IdMedicamento == 0)//Add
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"] + "medicamento");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress, medicamento);
                    postTask.Wait();
                    var result = postTask.Result;
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
                    var putTask = client.PutAsJsonAsync("medicamento/" + medicamento.IdMedicamento, medicamento);
                    putTask.Wait();
                    var result = putTask.Result;
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
                var result = deleteTask.Result;
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

        public ActionResult AddFarmacos(int IdMedicamento)
        {
            bool existe = false;
            ML.Venta carrito = new ML.Venta();
            carrito.ListaMedicamentos = new List<object>();
            ML.Result result = BL.Medicamento.GetById(IdMedicamento);
            if (Session["ListaMedicamentos"] == null)//session
            {
                if (result.Correct)
                {
                    ML.Medicamento medicamento = (ML.Medicamento)result.Object;
                    medicamento.Piezas = 1;
                    carrito.ListaMedicamentos.Add(medicamento);
                    Session["ListaMedicamentos"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrito.ListaMedicamentos);
                }
            }
            else
            {
                ML.Medicamento medicamento = (ML.Medicamento)result.Object;
                GetCarrito(carrito);
                foreach (ML.Medicamento item in carrito.ListaMedicamentos)
                {
                    if (medicamento.IdMedicamento == item.IdMedicamento)
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
                if (existe == true)
                {
                    Session["ListaMedicamentos"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrito.ListaMedicamentos);
                }
                else
                {
                    medicamento.Piezas = 1;
                    carrito.ListaMedicamentos.Add(medicamento);
                    Session["ListaMedicamentos"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrito.ListaMedicamentos);

                }
            }
            return RedirectToAction("Farmacos");
        }
        [HttpGet]
        public ActionResult Farmacos()
        {
            ML.Venta carrito = new ML.Venta();
            carrito.ListaMedicamentos = new List<object>();

            if (Session["ListaMedicamentos"] == null)
            {
                return View(carrito);
            }
            else
            {
                GetCarrito(carrito);
                return View(carrito);
            }

        }
        public ML.Venta GetCarrito(ML.Venta carrito)
        {
            var Lista = Session["ListaMedicamentos"];

            var carritoSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(Lista.ToString());
            foreach (var obj in carritoSession)
            {
                ML.Medicamento objMedicamento = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(obj.ToString());
                carrito.ListaMedicamentos.Add(objMedicamento);
            }
            return carrito;
        }
        [HttpPost]
        public ActionResult GeneratePDF(int Piezas, string NombreCompleto,int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            ML.Venta carrito = new ML.Venta();
            carrito.ListaMedicamentos = new List<object>();
            GetCarrito(carrito);

            string rutaPDF = Path.GetTempFileName() + ".pdf";
            using (PdfDocument pdf = new PdfDocument(new PdfWriter(rutaPDF)))
            {
                using (Document document = new Document(pdf))
                {
                    document.Add(new Paragraph("Resumen de compra de medicamentos"));
                    document.Add(new Paragraph(NombreCompleto));
                    Table table = new Table(4);
                    table.SetWidth(UnitValue.CreatePercentValue(100));
                    
                    table.AddHeaderCell("SKU");
                    table.AddHeaderCell("Medicamento");
                    table.AddHeaderCell("Precio");
                    table.AddHeaderCell("Pzs");
                    foreach (ML.Medicamento medicamentoPDF in carrito.ListaMedicamentos)
                    {
                        medicamentoPDF.Piezas = Piezas;

                        var cantidadReal= medicamentoPDF.Cantidad;
                        var totalReal = medicamentoPDF.Total;

                        medicamentoPDF.Cantidad=Piezas;

                        medicamentoPDF.Total = medicamentoPDF.Piezas * medicamentoPDF.Precio;

                        ML.Result resultInsert = BL.Medicamento.UsuarioMedicamentoAdd(medicamentoPDF, usuario);
                        medicamentoPDF.Cantidad = cantidadReal-Piezas;
                        medicamentoPDF.Total = medicamentoPDF.Cantidad * medicamentoPDF.Precio;

                        ML.Result resultUpdate=BL.Medicamento.Update(medicamentoPDF);
                        table.AddCell(medicamentoPDF.IdMedicamento.ToString());
                        table.AddCell(medicamentoPDF.NombreMedicamento);
                        table.AddCell(medicamentoPDF.Precio.ToString());
                        table.AddCell(medicamentoPDF.Piezas.ToString());

                    }
                    document.Add(table);
                    
                }
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaPDF);
            System.IO.File.Delete(rutaPDF);
            //Session["ListaMedicamentos"] = null;
            return new FileStreamResult(new MemoryStream(fileBytes), "application/pdf")
            {
                FileDownloadName = "ReporteMedicamentos"
            };

        }

    }
}