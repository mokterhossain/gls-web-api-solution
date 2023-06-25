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
    public class QCAnalyticDataService
    {
        #region Common Methods

        public List<QCAnalyticData> All()
        {
            List<QCAnalyticData> QCAnalyticData = new List<QCAnalyticData>();

            using (GLSPMContext db = new GLSPMContext())
            {
                QCAnalyticData = db.QCAnalyticDatas.OrderBy(f => f.Id).AsParallel().ToList();

                if (QCAnalyticData == null)
                {
                    QCAnalyticData = new List<QCAnalyticData>();
                }
                return QCAnalyticData;
            }
        }

        public QCAnalyticData GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                QCAnalyticData QCAnalyticData = db.QCAnalyticDatas.SingleOrDefault(u => u.Id == userRoleId);
                return QCAnalyticData;
            }
        }
        public QCAnalyticData GetBySampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                QCAnalyticData QCAnalyticData = db.QCAnalyticDatas.SingleOrDefault(u => u.SampleId == sampleId);
                return QCAnalyticData;
            }
        }
        public Response<QCAnalyticData> Add(QCAnalyticData QCAnalyticData)
        {
            Response<QCAnalyticData> addResponse = new Response<QCAnalyticData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.QCAnalyticDatas.Add(QCAnalyticData);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = QCAnalyticData;
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
                    addResponse.Result = QCAnalyticData;
                }
                return addResponse;
            }
        }

        public Response<QCAnalyticData> Update(QCAnalyticData QCAnalyticData)
        {
            Response<QCAnalyticData> updateResponse = new Response<QCAnalyticData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    QCAnalyticData updateQCAnalyticData = db.QCAnalyticDatas.SingleOrDefault(u => u.Id == QCAnalyticData.Id);
                    if (updateQCAnalyticData != null)
                    {
                        //updateQCAnalyticData = userRole;
                        updateQCAnalyticData.Id = QCAnalyticData.Id;
                        updateQCAnalyticData.SampleId = QCAnalyticData.SampleId;
                        updateQCAnalyticData.DataPoint1Value = QCAnalyticData.DataPoint1Value;
                        updateQCAnalyticData.DataPoint2Value = QCAnalyticData.DataPoint2Value;
                        updateQCAnalyticData.QCPercentage = QCAnalyticData.QCPercentage;
                        updateQCAnalyticData.QCStatus = QCAnalyticData.QCStatus;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = QCAnalyticData;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = QCAnalyticData;
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
                    updateResponse.Result = QCAnalyticData;
                }
                return updateResponse;
            }
        }

        public Response<QCAnalyticData> Delete(long Id)
        {
            Response<QCAnalyticData> deleteResponse = new Response<QCAnalyticData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    QCAnalyticData deleteQCAnalyticData = db.QCAnalyticDatas.SingleOrDefault(u => u.Id == Id);
                    if (deleteQCAnalyticData != null)
                    {
                        db.QCAnalyticDatas.Remove(deleteQCAnalyticData);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteQCAnalyticData;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteQCAnalyticData;
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
        //public List<ProjectSampleQCAnalyticDataViewModel> GetAllProjectSampleQCAnalyticDataData(long sampleId)
        //{
        //    List<ProjectSampleQCAnalyticDataViewModel> projectViewModel = new List<ProjectSampleQCAnalyticDataViewModel>();
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
        //        try
        //        {
        //            projectViewModel = db.Database.SqlQuery<ProjectSampleQCAnalyticDataViewModel>("GLS_GetAllProjectSampleQCAnalyticDataData @SampleId", sampleIdParameter).ToList<ProjectSampleQCAnalyticDataViewModel>();
        //        }
        //        catch (Exception ex) { }
        //        if (projectViewModel == null)
        //        {
        //            projectViewModel = new List<ProjectSampleQCAnalyticDataViewModel>();
        //        }
        //        return projectViewModel;
        //    }
        //}

        #endregion
    }
}
