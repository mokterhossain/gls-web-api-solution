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
    public class MatrixService
    {
        #region Common Methods

        public List<Matrix> All()
        {
            List<Matrix> Matrix = new List<Matrix>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Matrix = db.Matrixs.OrderBy(f => f.Id).AsParallel().ToList();

                if (Matrix == null)
                {
                    Matrix = new List<Matrix>();
                }
                return Matrix;
            }
        }

        public Matrix GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Matrix Matrix = db.Matrixs.SingleOrDefault(u => u.Id == userRoleId);
                return Matrix;
            }
        }
        public Response<Matrix> Add(Matrix Matrix)
        {
            Response<Matrix> addResponse = new Response<Matrix>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Matrixs.Add(Matrix);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Matrix;
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
                    addResponse.Result = Matrix;
                }
                return addResponse;
            }
        }

        public Response<Matrix> Update(Matrix Matrix)
        {
            Response<Matrix> updateResponse = new Response<Matrix>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Matrix updateMatrix = db.Matrixs.SingleOrDefault(u => u.Id == Matrix.Id);
                    if (updateMatrix != null)
                    {
                        //updateMatrix = userRole;
                        updateMatrix.Id = Matrix.Id;
                        updateMatrix.Name = Matrix.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = Matrix;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = Matrix;
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
                    updateResponse.Result = Matrix;
                }
                return updateResponse;
            }
        }

        public Response<Matrix> Delete(long Id)
        {
            Response<Matrix> deleteResponse = new Response<Matrix>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Matrix deleteMatrix = db.Matrixs.SingleOrDefault(u => u.Id == Id);
                    if (deleteMatrix != null)
                    {
                        db.Matrixs.Remove(deleteMatrix);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMatrix;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMatrix;
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
