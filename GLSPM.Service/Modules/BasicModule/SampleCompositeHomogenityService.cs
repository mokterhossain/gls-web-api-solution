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
    public class SampleCompositeHomogenityService
    {
        #region Common Methods

        public List<SampleCompositeHomogenity> All()
        {
            List<SampleCompositeHomogenity> SampleCompositeHomogenity = new List<SampleCompositeHomogenity>();

            using (GLSPMContext db = new GLSPMContext())
            {
                SampleCompositeHomogenity = db.SampleCompositeHomogenitys.OrderBy(f => f.Id).AsParallel().ToList();

                if (SampleCompositeHomogenity == null)
                {
                    SampleCompositeHomogenity = new List<SampleCompositeHomogenity>();
                }
                return SampleCompositeHomogenity;
            }
        }

        public SampleCompositeHomogenity GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleCompositeHomogenity SampleCompositeHomogenity = db.SampleCompositeHomogenitys.SingleOrDefault(u => u.Id == userRoleId);
                return SampleCompositeHomogenity;
            }
        }
        public SampleCompositeHomogenity GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampleCompositeHomogenity SampleCompositeHomogenity = db.SampleCompositeHomogenitys.SingleOrDefault(u => u.Name == name);
                return SampleCompositeHomogenity;
            }
        }
        public Response<SampleCompositeHomogenity> Add(SampleCompositeHomogenity SampleCompositeHomogenity)
        {
            Response<SampleCompositeHomogenity> addResponse = new Response<SampleCompositeHomogenity>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.SampleCompositeHomogenitys.Add(SampleCompositeHomogenity);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = SampleCompositeHomogenity;
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
                    addResponse.Result = SampleCompositeHomogenity;
                }
                return addResponse;
            }
        }

        public Response<SampleCompositeHomogenity> Update(SampleCompositeHomogenity SampleCompositeHomogenity)
        {
            Response<SampleCompositeHomogenity> updateResponse = new Response<SampleCompositeHomogenity>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleCompositeHomogenity updateSampleCompositeHomogenity = db.SampleCompositeHomogenitys.SingleOrDefault(u => u.Id == SampleCompositeHomogenity.Id);
                    if (updateSampleCompositeHomogenity != null)
                    {
                        //updateSampleCompositeHomogenity = userRole;
                        updateSampleCompositeHomogenity.Id = SampleCompositeHomogenity.Id;
                        updateSampleCompositeHomogenity.Name = SampleCompositeHomogenity.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = SampleCompositeHomogenity;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = SampleCompositeHomogenity;
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
                    updateResponse.Result = SampleCompositeHomogenity;
                }
                return updateResponse;
            }
        }

        public Response<SampleCompositeHomogenity> Delete(long Id)
        {
            Response<SampleCompositeHomogenity> deleteResponse = new Response<SampleCompositeHomogenity>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampleCompositeHomogenity deleteSampleCompositeHomogenity = db.SampleCompositeHomogenitys.SingleOrDefault(u => u.Id == Id);
                    if (deleteSampleCompositeHomogenity != null)
                    {
                        db.SampleCompositeHomogenitys.Remove(deleteSampleCompositeHomogenity);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteSampleCompositeHomogenity;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteSampleCompositeHomogenity;
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
