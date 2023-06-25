using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.BasicModule
{
    public class SampleLayerService
    {
        #region Common Methods

        public List<SampleLayer> All()
        {
            List<SampleLayer> SampleLayer = new List<SampleLayer>();

            using (GLSPMContext db = new GLSPMContext())
            {
                SampleLayer = db.SampleLayers.OrderBy(f => f.Id).AsParallel().ToList();

                if (SampleLayer == null)
                {
                    SampleLayer = new List<SampleLayer>();
                }
                return SampleLayer;
            }
        }

        public SampleLayer GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleLayer SampleLayer = db.SampleLayers.SingleOrDefault(u => u.Id == userRoleId);
                return SampleLayer;
            }
        }
        public SampleLayer GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleLayer SampleLayer = db.SampleLayers.SingleOrDefault(u => u.Name == name);
                return SampleLayer;
            }
        }
        public Response<SampleLayer> Add(SampleLayer SampleLayer)
        {
            Response<SampleLayer> addResponse = new Response<SampleLayer>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.SampleLayers.Add(SampleLayer);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = SampleLayer;
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This role Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the role Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = SampleLayer;
                }
                return addResponse;
            }
        }

        public Response<SampleLayer> Update(SampleLayer SampleLayer)
        {
            Response<SampleLayer> updateResponse = new Response<SampleLayer>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleLayer updateSampleLayer = db.SampleLayers.SingleOrDefault(u => u.Id == SampleLayer.Id);
                    if (updateSampleLayer != null)
                    {
                        //updateSampleLayer = userRole;
                        updateSampleLayer.Id = SampleLayer.Id;
                        updateSampleLayer.Name = SampleLayer.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = SampleLayer;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = SampleLayer;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = "Role Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This role Info already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the role Info: " + ex.Message;
                    }
                    updateResponse.Result = SampleLayer;
                }
                return updateResponse;
            }
        }

        public Response<SampleLayer> Delete(long Id)
        {
            Response<SampleLayer> deleteResponse = new Response<SampleLayer>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleLayer deleteSampleLayer = db.SampleLayers.SingleOrDefault(u => u.Id == Id);
                    if (deleteSampleLayer != null)
                    {
                        db.SampleLayers.Remove(deleteSampleLayer);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteSampleLayer;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteSampleLayer;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = "Role Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This role Info currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting role Info: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }
                return deleteResponse;
            }
        }

        #endregion Common Methods
    }
}
