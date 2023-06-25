using GLSPM.Data;
using GLSPM.Data.Modules.FinancialModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.FinancialModule
{
    public class ExpenseTypeService
    {
        #region Common Methods

        public List<ExpenseType> All()
        {
            List<ExpenseType> ExpenseType = new List<ExpenseType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ExpenseType = db.ExpenseTypes.OrderBy(f => f.TypeId).AsParallel().ToList();

                if (ExpenseType == null)
                {
                    ExpenseType = new List<ExpenseType>();
                }
                return ExpenseType;
            }
        }

        public ExpenseType GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ExpenseType ExpenseType = db.ExpenseTypes.SingleOrDefault(u => u.TypeId == userRoleId);
                return ExpenseType;
            }
        }
        public Response<ExpenseType> Add(ExpenseType ExpenseType)
        {
            Response<ExpenseType> addResponse = new Response<ExpenseType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ExpenseTypes.Add(ExpenseType);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ExpenseType;
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
                    addResponse.Result = ExpenseType;
                }
                return addResponse;
            }
        }

        public Response<ExpenseType> Update(ExpenseType ExpenseType)
        {
            Response<ExpenseType> updateResponse = new Response<ExpenseType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ExpenseType updateExpenseType = db.ExpenseTypes.SingleOrDefault(u => u.TypeId == ExpenseType.TypeId);
                    if (updateExpenseType != null)
                    {
                        //updateExpenseType = userRole;
                        updateExpenseType.TypeId = ExpenseType.TypeId;
                        updateExpenseType.Name = ExpenseType.Name;
                        updateExpenseType.Code = ExpenseType.Code;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ExpenseType;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ExpenseType;
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
                    updateResponse.Result = ExpenseType;
                }
                return updateResponse;
            }
        }

        public Response<ExpenseType> Delete(long Id)
        {
            Response<ExpenseType> deleteResponse = new Response<ExpenseType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ExpenseType deleteExpenseType = db.ExpenseTypes.SingleOrDefault(u => u.TypeId == Id);
                    if (deleteExpenseType != null)
                    {
                        db.ExpenseTypes.Remove(deleteExpenseType);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteExpenseType;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteExpenseType;
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
