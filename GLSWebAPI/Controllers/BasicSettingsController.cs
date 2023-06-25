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
    public class BasicSettingsController : ApiController
    {
        #region Asbestos Percentage
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/BasicSettings/GetAsbestosPercentage")]
        public HttpResponseMessage GetAsbestosPercentage(int start, int limit)
        {
            List<AsbestosPercentage> dataList = new AsbestosPercentageService().All();
            int total = dataList.Count();
            if (dataList != null)
            {
                dataList = dataList.Skip(start).Take(limit).ToList();
            }
            Response<AsbestosPercentage> response = new Response<AsbestosPercentage>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/BasicSettings/GetAsbestosPercentageDetail")]
        public HttpResponseMessage GetAsbestosPercentageDetail(int asbestosPercentageId)
        {
            List<AsbestosPercentageDetail> dataList = new AsbestosPercentageDetailService().GetByAsbestosPercentageID(asbestosPercentageId);
            
            Response<AsbestosPercentageDetail> response = new Response<AsbestosPercentageDetail>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
            
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/BasicSettings/SaveAsbestosPercentage")]
        public HttpResponseMessage SaveAsbestosPercentage([FromBody] AsbestosPercentage formData)
        {
            bool isSuccess = false;
            AsbestosPercentage saveResponseData = new AsbestosPercentage();
            if (formData.Id > 0)
            {
                var updateResponse = new AsbestosPercentageService().Update(formData);
                if (updateResponse.IsSuccess)
                {
                    isSuccess = true;
                    saveResponseData = updateResponse.Result;
                }
                List<AsbestosPercentageDetail> asbestosPercentageDetail = formData.AsbestosPercentageDetail;
                foreach(AsbestosPercentageDetail apd in asbestosPercentageDetail)
                {
                    if(apd.Id > 0)
                    {
                        var editResponse = new AsbestosPercentageDetailService().Update(apd);
                    }
                    else
                    {
                        var addResponse = new AsbestosPercentageDetailService().Add(apd);
                    }
                }
            }
            else
            {
                var addResponse = new AsbestosPercentageService().Add(formData);
                if (addResponse.IsSuccess)
                {
                    isSuccess = true;
                    saveResponseData = addResponse.Result;
                    List<AsbestosPercentageDetail> asbestosPercentageDetail = formData.AsbestosPercentageDetail;
                    foreach (AsbestosPercentageDetail apd in asbestosPercentageDetail)
                    {
                        apd.AsbestosPercentageId = addResponse.Result.Id;
                        var addResponse2 = new AsbestosPercentageDetailService().Add(apd);
                    }
                }
            }
            Response<AsbestosPercentage> response = new Response<AsbestosPercentage>();
            if (isSuccess == false)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = saveResponseData;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST", "DELETE")]
        [HttpGet]
        [Route("api/BasicSettings/DeleteAsbestosPercentage")]
        public HttpResponseMessage DeleteAsbestosPercentage(int Id)
        {
            bool isSuccess = false;
            AsbestosPercentage saveResponseData = new AsbestosPercentage();
            var deleteResponse = new AsbestosPercentageService().Delete(Id);
            if (deleteResponse.IsSuccess)
            {
                isSuccess = true;
                saveResponseData = deleteResponse.Result;
            }
            Response<AsbestosPercentage> response = new Response<AsbestosPercentage>();
            if (isSuccess == false)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = saveResponseData;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        #region Composite Non Asbestos Contents
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/BasicSettings/GetCompositeNonAsbestosContents")]
        public HttpResponseMessage GetCompositeNonAsbestosContents(int start, int limit)
        {
            List<CompositeNonAsbestosContents> dataList = new CompositeNonAsbestosContentsService().All();
            int total = dataList.Count();
            if (dataList != null)
            {
                dataList = dataList.Skip(start).Take(limit).ToList();
            }
            Response<CompositeNonAsbestosContents> response = new Response<CompositeNonAsbestosContents>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/BasicSettings/GetCompositeNonAsbestosContentsDetail")]
        public HttpResponseMessage GetCompositeNonAsbestosContentsDetail(int compositeNonAsbestosContentsId)
        {
            List<CompositeNonAsbestosContentsDetail> dataList = new CompositeNonAsbestosContentsDetailService().GetByAsbestosPercentageID(compositeNonAsbestosContentsId);

            Response<CompositeNonAsbestosContentsDetail> response = new Response<CompositeNonAsbestosContentsDetail>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/BasicSettings/SaveCompositeNonAsbestosContents")]
        public HttpResponseMessage SaveCompositeNonAsbestosContents([FromBody] CompositeNonAsbestosContents formData)
        {
            bool isSuccess = false;
            CompositeNonAsbestosContents saveResponseData = new CompositeNonAsbestosContents();
            if (formData.Id > 0)
            {
                var updateResponse = new CompositeNonAsbestosContentsService().Update(formData);
                if (updateResponse.IsSuccess)
                {
                    isSuccess = true;
                    saveResponseData = updateResponse.Result;
                }
                List<CompositeNonAsbestosContentsDetail> asbestosPercentageDetail = formData.CompositeNonAsbestosContentsDetail;
                foreach (CompositeNonAsbestosContentsDetail apd in asbestosPercentageDetail)
                {
                    if (apd.Id > 0)
                    {
                        var editResponse = new CompositeNonAsbestosContentsDetailService().Update(apd);
                    }
                    else
                    {
                        var addResponse = new CompositeNonAsbestosContentsDetailService().Add(apd);
                    }
                }
            }
            else
            {
                var addResponse = new CompositeNonAsbestosContentsService().Add(formData);
                if (addResponse.IsSuccess)
                {
                    isSuccess = true;
                    saveResponseData = addResponse.Result;
                    List<CompositeNonAsbestosContentsDetail> asbestosPercentageDetail = formData.CompositeNonAsbestosContentsDetail;
                    foreach (CompositeNonAsbestosContentsDetail apd in asbestosPercentageDetail)
                    {
                        apd.CompositeNonAsbestosContentsId = addResponse.Result.Id;
                        var addResponse2 = new CompositeNonAsbestosContentsDetailService().Add(apd);
                    }
                }
            }
            Response<CompositeNonAsbestosContents> response = new Response<CompositeNonAsbestosContents>();
            if (isSuccess == false)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = saveResponseData;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST", "DELETE")]
        [HttpGet]
        [Route("api/BasicSettings/DeleteCompositeNonAsbestosContents")]
        public HttpResponseMessage DeleteCompositeNonAsbestosContents(int Id)
        {
            bool isSuccess = false;
            CompositeNonAsbestosContents saveResponseData = new CompositeNonAsbestosContents();
            var deleteResponse = new CompositeNonAsbestosContentsService().Delete(Id);
            if (deleteResponse.IsSuccess)
            {
                isSuccess = true;
                saveResponseData = deleteResponse.Result;
            }
            Response<CompositeNonAsbestosContents> response = new Response<CompositeNonAsbestosContents>();
            if (isSuccess == false)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = saveResponseData;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/BasicSettings/GetBasicControls")]
        public HttpResponseMessage GetBasicControls()
        {
            BasicControl basicControl = new BasicControl();
            List<Employee> employeeList = new EmployeeService().All();
            List<Client> clientList = new ClientService().All();
            List<SampleLayer> samplelayerList = new SampleLayerService().All();
            List<SampleType> sampleTypeList = new SampleTypeService().All();
            List<ClientContactPerson> clientContact = new ClientContactPersonService().All();
            List<SampledBy> sampledBy = new SampledByService().All();
            List<PackageCode> packageCode = new PackageCodeService().All();
            List<Matrix> matrix = new MatrixService().All();
            List<Location> location = new LocationService().GetSomeLocation();
            List<Location> allLocation = new LocationService().All();
            List<SampleCompositeHomogenity> sampleCompositeHomogenity = new SampleCompositeHomogenityService().All();
            List<AsbestosPercentage> asbestosPercentage = new AsbestosPercentageService().All();
            List<CompositeNonAsbestosContents> compositeNonAsbestosContents = new CompositeNonAsbestosContentsService().All();

            basicControl.EmployeeList = employeeList.Where(x => x.IsActive == true).ToList();
            basicControl.ClientList = clientList;
            basicControl.SampleLayerList = samplelayerList;
            basicControl.SampleTypeList = sampleTypeList;
            basicControl.ClientContactPerson = clientContact;
            basicControl.SampledBy = sampledBy;
            basicControl.PackageCode = packageCode;
            basicControl.Matrix = matrix;
            basicControl.Location = location;
            basicControl.AllLocation = allLocation;
            basicControl.SampleCompositeHomogenity = sampleCompositeHomogenity;
            basicControl.AsbestosPercentage = asbestosPercentage;
            basicControl.CompositeNonAsbestosContents = compositeNonAsbestosContents;
            Response<BasicControl> response = new Response<BasicControl>();
            if(basicControl == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success";
                response.Result = basicControl;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public class BasicControl
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Client> ClientList { get; set; }
            public List<SampleLayer> SampleLayerList { get; set; }

            public List<SampleType> SampleTypeList { get; set; }
            public List<ClientContactPerson> ClientContactPerson { get; set; }
            public List<SampledBy> SampledBy { get; set; }

            public List<PackageCode> PackageCode { get; set; }
            public List<Matrix> Matrix { get; set; }
            public List<Location> Location { get; set; }
            public List<Location> AllLocation { get; set; }
            public List<SampleCompositeHomogenity> SampleCompositeHomogenity { get; set; }
            public List<AsbestosPercentage> AsbestosPercentage { get; set; }

            public List<CompositeNonAsbestosContents> CompositeNonAsbestosContents { get; set; }

        }
    }
}
