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
    public class ExpenseTransactionService
    {
        #region Common Methods

        public List<ExpenseTransaction> All()
        {
            List<ExpenseTransaction> ExpenseTransaction = new List<ExpenseTransaction>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ExpenseTransaction = db.ExpenseTransactions.OrderBy(f => f.ExpenseId).AsParallel().ToList();

                if (ExpenseTransaction == null)
                {
                    ExpenseTransaction = new List<ExpenseTransaction>();
                }
                return ExpenseTransaction;
            }
        }

        public ExpenseTransaction GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ExpenseTransaction ExpenseTransaction = db.ExpenseTransactions.SingleOrDefault(u => u.ExpenseId == userRoleId);
                return ExpenseTransaction;
            }
        }
        public Response<ExpenseTransaction> Add(ExpenseTransaction ExpenseTransaction)
        {
            Response<ExpenseTransaction> addResponse = new Response<ExpenseTransaction>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ExpenseTransactions.Add(ExpenseTransaction);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ExpenseTransaction;
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
                    addResponse.Result = ExpenseTransaction;
                }
                return addResponse;
            }
        }

        public Response<ExpenseTransaction> Update(ExpenseTransaction ExpenseTransaction)
        {
            Response<ExpenseTransaction> updateResponse = new Response<ExpenseTransaction>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ExpenseTransaction updateExpenseTransaction = db.ExpenseTransactions.SingleOrDefault(u => u.ExpenseId == ExpenseTransaction.ExpenseId);
                    if (updateExpenseTransaction != null)
                    {
                        //updateExpenseTransaction = userRole;
                        updateExpenseTransaction.ExpenseId = ExpenseTransaction.ExpenseId;
                        updateExpenseTransaction.ExpenseTypeId = ExpenseTransaction.ExpenseTypeId;
                        updateExpenseTransaction.DateOfTransaction = ExpenseTransaction.DateOfTransaction;
                        updateExpenseTransaction.Description = ExpenseTransaction.Description;
                        updateExpenseTransaction.ExpenseAmount = ExpenseTransaction.ExpenseAmount;
                        updateExpenseTransaction.UpdatedOn = ExpenseTransaction.UpdatedOn;
                        updateExpenseTransaction.UpdatedBy = ExpenseTransaction.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ExpenseTransaction;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ExpenseTransaction;
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
                    updateResponse.Result = ExpenseTransaction;
                }
                return updateResponse;
            }
        }

        public Response<ExpenseTransaction> Delete(long Id)
        {
            Response<ExpenseTransaction> deleteResponse = new Response<ExpenseTransaction>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ExpenseTransaction deleteExpenseTransaction = db.ExpenseTransactions.SingleOrDefault(u => u.ExpenseId == Id);
                    if (deleteExpenseTransaction != null)
                    {
                        db.ExpenseTransactions.Remove(deleteExpenseTransaction);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteExpenseTransaction;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteExpenseTransaction;
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
        #region Other
        public List<ExpenseTransactionViewModel> GetAllExpenseTransaction(int stratRowNumber, int endRowNumber,out int totalRecordCount)
        {
            List<ExpenseTransactionViewModel> ExpenseTransactionViewModel = new List<ExpenseTransactionViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    ExpenseTransactionViewModel = db.Database.SqlQuery<ExpenseTransactionViewModel>("GLS_GetAllExpenseTransaction @StratRowNumber,@EndRowNumber,@TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, totalRecordCountParameter).ToList<ExpenseTransactionViewModel>();
                }
                catch (Exception ex) { }
                if (ExpenseTransactionViewModel == null)
                {
                    ExpenseTransactionViewModel = new List<ExpenseTransactionViewModel>();
                }
                else
                {
                    totalRecordCount = Convert.ToInt32(totalRecordCountParameter.Value.ToString());
                }
                return ExpenseTransactionViewModel;
            }
        }
        #endregion
    }
}
