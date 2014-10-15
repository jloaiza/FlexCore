using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlexCoreRest.Controllers
{
    public class PagosController : ApiController
    {
        //POST /pagar
        //Reliza un nuevo pago
        public HttpResponseMessage PostPagar()
        {
            string _datosPost = Request.Content.ReadAsStringAsync().Result;
            HttpResponseMessage _request = Request.CreateResponse(HttpStatusCode.OK, _datosPost);
            _request.Headers.Add("Access-Control-Allow-Origin", "*");
            return _request;
        }
    }
}
