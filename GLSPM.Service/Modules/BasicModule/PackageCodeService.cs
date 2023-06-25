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
    public class PackageCodeService
    {
        #region Common Methods

        public List<PackageCode> All()
        {
            List<PackageCode> PackageCode = new List<PackageCode>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PackageCode = db.PackageCodes.OrderBy(f => f.Id).AsParallel().ToList();

                if (PackageCode == null)
                {
                    PackageCode = new List<PackageCode>();
                }
                return PackageCode;
            }
        }

        public PackageCode GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PackageCode PackageCode = db.PackageCodes.SingleOrDefault(u => u.Id == userRoleId);
                return PackageCode;
            }
        }
        public Response<PackageCode> Add(PackageCode PackageCode)
        {
            Response<PackageCode> addResponse = new Response<PackageCode>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.PackageCodes.Add(PackageCode);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = PackageCode;
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
                    addResponse.Result = PackageCode;
                }
                return addResponse;
            }
        }

        public Response<PackageCode> Update(PackageCode PackageCode)
        {
            Response<PackageCode> updateResponse = new Response<PackageCode>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PackageCode updatePackageCode = db.PackageCodes.SingleOrDefault(u => u.Id == PackageCode.Id);
                    if (updatePackageCode != null)
                    {
                        //updatePackageCode = userRole;
                        updatePackageCode.Id = PackageCode.Id;
                        updatePackageCode.Code = PackageCode.Code;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = PackageCode;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = PackageCode;
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
                    updateResponse.Result = PackageCode;
                }
                return updateResponse;
            }
        }

        public Response<PackageCode> Delete(long Id)
        {
            Response<PackageCode> deleteResponse = new Response<PackageCode>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PackageCode deletePackageCode = db.PackageCodes.SingleOrDefault(u => u.Id == Id);
                    if (deletePackageCode != null)
                    {
                        db.PackageCodes.Remove(deletePackageCode);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deletePackageCode;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deletePackageCode;
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
