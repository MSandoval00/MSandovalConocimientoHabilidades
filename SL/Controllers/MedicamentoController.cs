using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/medicamento")]
    public class MedicamentoController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Medicamento.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(ML.Medicamento medicamento)
        {
            ML.Result result=BL.Medicamento.Add(medicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdMedicamento}")]
        public IHttpActionResult Delete(int IdMedicamento)
        {
            ML.Result result = BL.Medicamento.Delete(IdMedicamento);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest,result);
            }
        }
        [HttpPut]
        [Route("{IdMedicamento}")]
        public IHttpActionResult Update(int IdMedicamento,[FromBody]ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Update(medicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{IdMedicamento}")]
        public IHttpActionResult GetById(int IdMedicamento)
        {
            ML.Result result=BL.Medicamento.GetById(IdMedicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
