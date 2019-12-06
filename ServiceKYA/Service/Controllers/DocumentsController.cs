using BussinessLayer.Documents;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels;

namespace Service.Controllers
{
    [Authorize]
    [RoutePrefix("api/documents")]
    public class DocumentsController : ApiController
    {
        BLDocuments doc = new BLDocuments();

        [HttpPost]
        [Route("insdocument")]
        public async Task<IHttpActionResult> InsertDocument(DocumentsVM au)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var filestring = Upload.FilePath(au.FileDocument);
            au.StringFile = filestring;
            var response = await doc.InsertDocuments(au);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deldocument")]
        public async Task<IHttpActionResult> DeleteDocument(Guid? IdDocument)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await doc.DeleteDocuments(IdDocument);
            return Ok(response);
        }


        [HttpGet]
        [Route("seldocument")]
        public async Task<IHttpActionResult> SelectDocument(Guid? IdOwner)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var response = await doc.SelectDocuments(IdOwner);
            return Ok(response);
        }


        [HttpPut]
        [Route("upddocument")]
        public async Task<IHttpActionResult> UpdateDocument(DocumentsUpdVM d)
        {
            //obtiene si tiene mensajes pendientes de leer en general retorna true o false
            var filestring = Upload.FilePath(d.FileDocument);
            d.StringFile = filestring;
            var response = await doc.UpdateDocuments(d);
            return Ok(response);
        }

    }
}
