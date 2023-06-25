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
    public class MoldService
    {
        #region Common Methods

        public List<Mold> All()
        {
            List<Mold> Mold = new List<Mold>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Mold = db.Molds.OrderBy(f => f.Id).AsParallel().ToList();

                if (Mold == null)
                {
                    Mold = new List<Mold>();
                }
                return Mold;
            }
        }

        public Mold GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Mold Mold = db.Molds.SingleOrDefault(u => u.Id == userRoleId);
                return Mold;
            }
        }
        public Mold GetByProjectID(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Mold Mold = db.Molds.SingleOrDefault(u => u.ProjectId == projectId);
                if (Mold != null)
                {
                    List<MoldSample> moldSamples = db.MoldSamples.Where(s => s.MoldId == Mold.Id).ToList();
                    moldSamples = moldSamples.OrderBy(ms => ms.SerialNo).ToList();
                    Mold.MoldSamples = moldSamples;
                    Mold.MoldSamples.ForEach(item =>
                    {
                        //List<MoldSampleDetail> MoldSampleDetails = db.MoldSampleDetails.Where(ms => ms.MoldSampleId == item.Id).ToList();
                        List<MoldSampleDetail> MoldSampleDetails = new MoldSampleDetailService().GetMoldSampleDetailBySample(item.Id).ToList();
                        item.MoldSampleDetails = MoldSampleDetails;

                    });
                }
                else
                {
                    Mold = new Mold();
                    Mold.Id = 0;
                    Mold.ProjectId = projectId;
                    Mold.ReportName = "MoldReport-" + projectId;
                    Mold.Comments = "";
                    List<ProjectSample> projectSample = db.ProjectSamples.Where(s => s.ProjectId == projectId).ToList();
                    projectSample = projectSample.OrderBy(ps => ps.SerialNo).ToList();
                    List<MoldSample> moldSampleList = new List<MoldSample>();
                    int sampleCount = 1;
                    projectSample.ForEach(item =>
                    {
                        MoldSample moldSample = new MoldSample();
                        moldSample.Id = (-1)*sampleCount;
                        moldSample.SampleId = item.SampleId;
                        moldSample.Location = item.Location;
                        moldSample.LabId = item.LabId;
                        moldSample.IsQC = item.IsQC;
                        moldSample.SerialNo = (int)item.SerialNo;
                        List<MoldSporeType> moldSporeType = db.MoldSporeTypes.OrderBy(m=> m.SerialNo).ToList();
                        List<MoldSampleDetail> moldSampleDetailList = new List<MoldSampleDetail>();
                        moldSporeType.ForEach(sporeType =>
                        {
                            MoldSampleDetail moldSampleDetail = new MoldSampleDetail();
                            moldSampleDetail.SporeTypeId = sporeType.Id;
                            moldSampleDetailList.Add(moldSampleDetail);
                        });
                        
                        moldSample.MoldSampleDetails = moldSampleDetailList;
                        moldSampleList.Add(moldSample);
                        if (item.IsQC == true)
                        {
                            sampleCount++;
                            moldSample = new MoldSample();
                            moldSample.Id = (-1) * sampleCount;
                            moldSample.SampleId = item.SampleId;
                            moldSample.Location = item.Location + "(Dup)";
                            moldSample.LabId = item.LabId;
                            moldSample.IsQC = item.IsQC;
                            moldSample.IsDuplicate = true;
                            moldSample.SerialNo = (double)item.SerialNo + 0.5;
                            moldSporeType = db.MoldSporeTypes.OrderBy(m => m.SerialNo).ToList();
                            moldSampleDetailList = new List<MoldSampleDetail>();
                            moldSporeType.ForEach(sporeType =>
                            {
                                MoldSampleDetail moldSampleDetail = new MoldSampleDetail();
                                moldSampleDetail.SporeTypeId = sporeType.Id;
                                moldSampleDetailList.Add(moldSampleDetail);
                            });
                            moldSample.MoldSampleDetails = moldSampleDetailList;
                            moldSampleList.Add(moldSample);

                        }
                        sampleCount++;
                    });
                    Mold.MoldSamples = moldSampleList;
                }
                return Mold;
            }
        }
        public Mold GetByProjectIDForTapeLift(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Mold Mold = db.Molds.SingleOrDefault(u => u.ProjectId == projectId);
                if (Mold != null)
                {
                    List<MoldSample> moldSamples = db.MoldSamples.Where(s => s.MoldId == Mold.Id).ToList();
                    Mold.MoldSamples = moldSamples;
                    Mold.MoldSamples.ForEach(item =>
                    {
                        List<MoldTapeLiftSampleDetail> MoldSampleDetails = db.MoldTapeLiftSampleDetails.Where(ms => ms.MoldSampleId == item.Id).ToList();
                        item.MoldTapeLiftSampleDetails = MoldSampleDetails;

                    });
                }
                else
                {
                    Mold = new Mold();
                    Mold.Id = 0;
                    Mold.ProjectId = projectId;
                    Mold.ReportName = "MoldReport-" + projectId;
                    Mold.Comments = "";
                    List<ProjectSample> projectSample = db.ProjectSamples.Where(s => s.ProjectId == projectId).ToList();
                    List<MoldSample> moldSampleList = new List<MoldSample>();
                    projectSample.ForEach(item =>
                    {
                        MoldSample moldSample = new MoldSample();
                        moldSample.SampleId = item.SampleId;
                        moldSample.Location = item.Location;
                        moldSample.LabId = item.LabId;
                        List<MoldSporeType> moldSporeType = db.MoldSporeTypes.OrderBy(m => m.SerialNo).ToList();
                        List<MoldTapeLiftSampleDetail> moldSampleDetailList = new List<MoldTapeLiftSampleDetail>();

                        moldSporeType.ForEach(sporeType =>
                        {
                            MoldTapeLiftSampleDetail moldSampleDetail = new MoldTapeLiftSampleDetail();
                            moldSampleDetail.SporeTypeId = sporeType.Id;
                            moldSampleDetailList.Add(moldSampleDetail);
                        });
                        moldSample.MoldTapeLiftSampleDetails = moldSampleDetailList;
                        moldSampleList.Add(moldSample);
                    });
                    Mold.MoldSamples = moldSampleList;
                }
                return Mold;
            }
        }
        public Response<Mold> Add(Mold Mold)
        {
            Response<Mold> addResponse = new Response<Mold>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Molds.Add(Mold);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Mold;
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
                    addResponse.Result = Mold;
                }
                return addResponse;
            }
        }

        public Response<Mold> Update(Mold mold)
        {
            Response<Mold> updateResponse = new Response<Mold>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Mold updateMold = db.Molds.SingleOrDefault(u => u.Id == mold.Id);
                    if (updateMold != null)
                    {
                        //updateMold = userRole;
                        updateMold.Id = mold.Id;
                        updateMold.ProjectId = mold.ProjectId;
                        updateMold.ReportName = mold.ReportName;
                        updateMold.Comments = mold.Comments;
                        // updateMold.MoldSamples = mold.MoldSamples;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = mold;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = mold;
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
                    updateResponse.Result = mold;
                }
                return updateResponse;
            }
        }

        public Response<Mold> Delete(long Id)
        {
            Response<Mold> deleteResponse = new Response<Mold>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Mold deleteMold = db.Molds.SingleOrDefault(u => u.Id == Id);
                    if (deleteMold != null)
                    {
                        db.Molds.Remove(deleteMold);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMold;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMold;
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
        public List<MoldViewModel> GetDataForMoldReport(long projectId)
        {
            List<MoldViewModel> projectViewModel = new List<MoldViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldViewModel>("GLS_GetDataForMoldReport @ProjectId", projectIdParameter).ToList<MoldViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<MoldViewModel> GetDataForMoldReportPrint(long projectId)
        {
            List<MoldViewModel> projectViewModel = new List<MoldViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldViewModel>("GLS_GetDataForMoldReportPrint @ProjectId", projectIdParameter).ToList<MoldViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<MoldTapeLiftReportModel> GetDataForMoldTapeLiftReport(long projectId)
        {
            List<MoldTapeLiftReportModel> projectViewModel = new List<MoldTapeLiftReportModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldTapeLiftReportModel>("GLS_GetDataForMoldTapeLiftReport @ProjectId", projectIdParameter).ToList<MoldTapeLiftReportModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldTapeLiftReportModel>();
                }
                return projectViewModel;
            }
        }
        public List<MoldTapeLiftReportModel> GetDataForMoldTapeLiftReportPrint(long projectId)
        {
            List<MoldTapeLiftReportModel> projectViewModel = new List<MoldTapeLiftReportModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldTapeLiftReportModel>("GLS_GetDataForMoldTapeLiftReportPrint @ProjectId", projectIdParameter).ToList<MoldTapeLiftReportModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldTapeLiftReportModel>();
                }
                return projectViewModel;
            }
        }
    }
}
