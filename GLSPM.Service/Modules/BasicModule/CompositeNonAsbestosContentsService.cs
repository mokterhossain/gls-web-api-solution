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
    public class CompositeNonAsbestosContentsService
    {
        #region Common Methods

        public List<CompositeNonAsbestosContents> All()
        {
            List<CompositeNonAsbestosContents> CompositeNonAsbestosContents = new List<CompositeNonAsbestosContents>();

            using (GLSPMContext db = new GLSPMContext())
            {
                CompositeNonAsbestosContents = db.CompositeNonAsbestosContentss.OrderBy(f => f.Id).AsParallel().ToList();

                if (CompositeNonAsbestosContents == null)
                {
                    CompositeNonAsbestosContents = new List<CompositeNonAsbestosContents>();
                }
                return CompositeNonAsbestosContents;
            }
        }

        public CompositeNonAsbestosContents GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                CompositeNonAsbestosContents CompositeNonAsbestosContents = db.CompositeNonAsbestosContentss.SingleOrDefault(u => u.Id == userRoleId);
                return CompositeNonAsbestosContents;
            }
        }
        public CompositeNonAsbestosContents GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                CompositeNonAsbestosContents CompositeNonAsbestosContents = db.CompositeNonAsbestosContentss.SingleOrDefault(u => u.Name == name);
                return CompositeNonAsbestosContents;
            }
        }
        public Response<CompositeNonAsbestosContents> Add(CompositeNonAsbestosContents CompositeNonAsbestosContents)
        {
            Response<CompositeNonAsbestosContents> addResponse = new Response<CompositeNonAsbestosContents>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.CompositeNonAsbestosContentss.Add(CompositeNonAsbestosContents);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = CompositeNonAsbestosContents;
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
                    addResponse.Result = CompositeNonAsbestosContents;
                }
                return addResponse;
            }
        }

        public Response<CompositeNonAsbestosContents> Update(CompositeNonAsbestosContents CompositeNonAsbestosContents)
        {
            Response<CompositeNonAsbestosContents> updateResponse = new Response<CompositeNonAsbestosContents>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    CompositeNonAsbestosContents updateCompositeNonAsbestosContents = db.CompositeNonAsbestosContentss.SingleOrDefault(u => u.Id == CompositeNonAsbestosContents.Id);
                    if (updateCompositeNonAsbestosContents != null)
                    {
                        //updateCompositeNonAsbestosContents = userRole;
                        updateCompositeNonAsbestosContents.Id = CompositeNonAsbestosContents.Id;
                        updateCompositeNonAsbestosContents.Name = CompositeNonAsbestosContents.Name;
                        updateCompositeNonAsbestosContents.NumericValue = CompositeNonAsbestosContents.NumericValue;
                        updateCompositeNonAsbestosContents.UpdatedOn = DateTime.Now;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = CompositeNonAsbestosContents;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = CompositeNonAsbestosContents;
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
                    updateResponse.Result = CompositeNonAsbestosContents;
                }
                return updateResponse;
            }
        }

        public Response<CompositeNonAsbestosContents> Delete(long Id)
        {
            Response<CompositeNonAsbestosContents> deleteResponse = new Response<CompositeNonAsbestosContents>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    CompositeNonAsbestosContents deleteCompositeNonAsbestosContents = db.CompositeNonAsbestosContentss.SingleOrDefault(u => u.Id == Id);
                    if (deleteCompositeNonAsbestosContents != null)
                    {
                        db.CompositeNonAsbestosContentss.Remove(deleteCompositeNonAsbestosContents);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteCompositeNonAsbestosContents;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteCompositeNonAsbestosContents;
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
