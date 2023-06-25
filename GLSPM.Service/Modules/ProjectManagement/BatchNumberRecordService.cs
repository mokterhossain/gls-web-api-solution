using GLSPM.Data;
using GLSPM.Data.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.ProjectManagement
{
    public class BatchNumberRecordService
    {
        #region Common Methods

        public List<BatchNumberRecord> All()
        {
            List<BatchNumberRecord> BatchNumberRecord = new List<BatchNumberRecord>();

            using (GLSPMContext db = new GLSPMContext())
            {
                BatchNumberRecord = db.BatchNumberRecords.OrderBy(f => f.Id).AsParallel().ToList();

                if (BatchNumberRecord == null)
                {
                    BatchNumberRecord = new List<BatchNumberRecord>();
                }
                return BatchNumberRecord;
            }
        }

        public BatchNumberRecord GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                BatchNumberRecord BatchNumberRecord = db.BatchNumberRecords.SingleOrDefault(u => u.Id == userRoleId);
                return BatchNumberRecord;
            }
        }
        public BatchNumberRecord GetByBatchNumber(string batchNumber)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                BatchNumberRecord BatchNumberRecord = db.BatchNumberRecords.SingleOrDefault(u => u.BatchNumber == batchNumber);
                return BatchNumberRecord;
            }
        }
        public Response<BatchNumberRecord> Add(BatchNumberRecord BatchNumberRecord)
        {
            Response<BatchNumberRecord> addResponse = new Response<BatchNumberRecord>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.BatchNumberRecords.Add(BatchNumberRecord);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = BatchNumberRecord;
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
                    addResponse.Result = BatchNumberRecord;
                }
                return addResponse;
            }
        }

        public Response<BatchNumberRecord> Update(BatchNumberRecord BatchNumberRecord)
        {
            Response<BatchNumberRecord> updateResponse = new Response<BatchNumberRecord>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    BatchNumberRecord updateBatchNumberRecord = db.BatchNumberRecords.SingleOrDefault(u => u.Id == BatchNumberRecord.Id);
                    if (updateBatchNumberRecord != null)
                    {
                        //updateBatchNumberRecord = userRole;
                        updateBatchNumberRecord.Id = BatchNumberRecord.Id;
                        updateBatchNumberRecord.BatchNumber = BatchNumberRecord.BatchNumber;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = BatchNumberRecord;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = BatchNumberRecord;
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
                    updateResponse.Result = BatchNumberRecord;
                }
                return updateResponse;
            }
        }

        public Response<BatchNumberRecord> Delete(long Id)
        {
            Response<BatchNumberRecord> deleteResponse = new Response<BatchNumberRecord>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    BatchNumberRecord deleteBatchNumberRecord = db.BatchNumberRecords.SingleOrDefault(u => u.Id == Id);
                    if (deleteBatchNumberRecord != null)
                    {
                        db.BatchNumberRecords.Remove(deleteBatchNumberRecord);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteBatchNumberRecord;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteBatchNumberRecord;
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
        #region Other Methods
        public List<BatchNumberRecord> GetAllBatchNumber(int stratRowNumber, int endRowNumber, string batchNumber, out int totalRecordCount)
        {
            List<BatchNumberRecord> projectViewModel = new List<BatchNumberRecord>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<BatchNumberRecord>("GLS_GetAllBatchNumber @StratRowNumber,@EndRowNumber,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, batchNumberParameter, totalRecordCountParameter).ToList<BatchNumberRecord>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<BatchNumberRecord>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
