using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            ML.Result result=BL.Usuario.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(ML.Usuario usuario)
        {
            ML.Result result=BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdUsuario}")]
        public IHttpActionResult Update(int IdUsuario,[FromBody]ML.Usuario usuario)
        {
            ML.Result result=BL.Usuario.Update(usuario);
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
        [Route("{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result=BL.Usuario.Delete(IdUsuario);
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
        [Route("{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result=BL.Usuario.GetById(IdUsuario);
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
