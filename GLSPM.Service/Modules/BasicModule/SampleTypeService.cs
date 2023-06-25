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
    public class SampleTypeService
    {
        #region Common Methods

        public List<SampleType> All()
        {
            List<SampleType> SampleType = new List<SampleType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                SampleType = db.SampleTypes.OrderBy(f => f.Id).AsParallel().ToList();

                if (SampleType == null)
                {
                    SampleType = new List<SampleType>();
                }
                return SampleType;
            }
        }

        public SampleType GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleType SampleType = db.SampleTypes.SingleOrDefault(u => u.Id == userRoleId);
                return SampleType;
            }
        }
        public SampleType GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleType SampleType = db.SampleTypes.SingleOrDefault(u => u.Name == name);
                return SampleType;
            }
        }
        public Response<SampleType> Add(SampleType SampleType)
        {
            Response<SampleType> addResponse = new Response<SampleType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.SampleTypes.Add(SampleType);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = SampleType;
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
                    addResponse.Result = SampleType;
                }
                return addResponse;
            }
        }

        public Response<SampleType> Update(SampleType SampleType)
        {
            Response<SampleType> updateResponse = new Response<SampleType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleType updateSampleType = db.SampleTypes.SingleOrDefault(u => u.Id == SampleType.Id);
                    if (updateSampleType != null)
                    {
                        //updateSampleType = userRole;
                        updateSampleType.Id = SampleType.Id;
                        updateSampleType.Name = SampleType.Name;
                        updateSampleType.ItemCode = SampleType.ItemCode;
                        updateSampleType.Price = SampleType.Price;
                        updateSampleType.TATValue = SampleType.TATValue;
                        updateSampleType.ClientId = SampleType.ClientId;
                        updateSampleType.ClientName = SampleType.ClientName;
                        updateSampleType.ProjectType = SampleType.ProjectType;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = SampleType;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = SampleType;
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
                    updateResponse.Result = SampleType;
                }
                return updateResponse;
            }
        }

        public Response<SampleType> Delete(long Id)
        {
            Response<SampleType> deleteResponse = new Response<SampleType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleType deleteSampleType = db.SampleTypes.SingleOrDefault(u => u.Id == Id);
                    if (deleteSampleType != null)
                    {
                        db.SampleTypes.Remove(deleteSampleType);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteSampleType;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteSampleType;
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
        public List<SampleType> GetSampleTypeByClientId(int clientId)
        {
            List<SampleType> SampleType = new List<SampleType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var clientIdParameter = new SqlParameter { ParameterName = "ClientId", Value = clientId };
                try
                {
                    SampleType = db.Database.SqlQuery<SampleType>("GLS_GetSampleTypeByClientId @ClientId", clientIdParameter).ToList<SampleType>();
                }
                catch (Exception ex) { }
                if (SampleType == null)
                {
                    SampleType = new List<SampleType>();
                }
                return SampleType;
            }
        }
        public List<SampleType> GetSampleTypeByClientIdAndProjectType(int clientId, string projectType)
        {
            List<SampleType> SampleType = new List<SampleType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var clientIdParameter = new SqlParameter { ParameterName = "ClientId", Value = clientId };
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    SampleType = db.Database.SqlQuery<SampleType>("GLS_GetSampleTypeByClientIdAndProjectType @ClientId,@ProjectType", clientIdParameter, projectTypeParameter).ToList<SampleType>();
                }
                catch (Exception ex) { }
                if (SampleType == null)
                {
                    SampleType = new List<SampleType>();
                }
                return SampleType;
            }
        }
    }
}
