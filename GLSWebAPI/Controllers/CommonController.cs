using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using GLSPM.Service.Modules.BasicModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GLSWebAPI.Controllers
{
    public class CommonController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Common/GetAccountManagerForInvoice")]
        public HttpResponseMessage GetAccountManagerForInvoice()
        {
            //List<AsbestosPercentage> dataList = new AsbestosPercentageService().All();
            List<Employee> dataList = new EmployeeService().All();
            Response<Employee> response = new Response<Employee>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                dataList = dataList.Where(e => e.CanSignOnInvoice == true && e.IsActive == true).ToList();
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}