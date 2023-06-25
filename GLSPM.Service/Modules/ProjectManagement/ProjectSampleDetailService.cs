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
    public class ProjectSampleDetailService
    {
        #region Common Methods

        public List<ProjectSampleDetail> All()
        {
            List<ProjectSampleDetail> ProjectSampleDetail = new List<ProjectSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSampleDetail = db.ProjectSampleDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (ProjectSampleDetail == null)
                {
                    ProjectSampleDetail = new List<ProjectSampleDetail>();
                }
                return ProjectSampleDetail;
            }
        }

        public ProjectSampleDetail GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSampleDetail ProjectSampleDetail = db.ProjectSampleDetails.SingleOrDefault(u => u.Id == userRoleId);
                return ProjectSampleDetail;
            }
        }
        public List<ProjectSampleDetail> GetByProjectSampleID(long projectSampleId)
        {
            List<ProjectSampleDetail> ProjectSampleDetail = new List<ProjectSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSampleDetail = db.ProjectSampleDetails.OrderBy(f => f.Id).Where(p=>p.ProjectSampleId == projectSampleId).AsParallel().ToList();

                if (ProjectSampleDetail == null)
                {
                    ProjectSampleDetail = new List<ProjectSampleDetail>();
                }
                return ProjectSampleDetail;
            }
        }
        public Response<ProjectSampleDetail> Add(ProjectSampleDetail ProjectSampleDetail)
        {
            Response<ProjectSampleDetail> addResponse = new Response<ProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ProjectSampleDetails.Add(ProjectSampleDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ProjectSampleDetail;
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
                    addResponse.Result = ProjectSampleDetail;
                }
                return addResponse;
            }
        }

        public Response<ProjectSampleDetail> Update(ProjectSampleDetail ProjectSampleDetail)
        {
            Response<ProjectSampleDetail> updateResponse = new Response<ProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectSampleDetail updateProjectSample = db.ProjectSampleDetails.SingleOrDefault(u => u.Id == ProjectSampleDetail.Id);
                    if (updateProjectSample != null)
                    {
                        //updateProjectSample = userRole;
                        updateProjectSample.Id = ProjectSampleDetail.Id;
                        updateProjectSample.ProjectSampleId = ProjectSampleDetail.ProjectSampleId;
                        updateProjectSample.SampleTypeId = ProjectSampleDetail.SampleTypeId;
                        updateProjectSample.SampleType = ProjectSampleDetail.SampleType;
                        updateProjectSample.Homogeneity = ProjectSampleDetail.Homogeneity;
                        updateProjectSample.SampleContent = ProjectSampleDetail.SampleContent;
                        updateProjectSample.AbsestosPercentage = ProjectSampleDetail.AbsestosPercentage;
                        updateProjectSample.AbsestosPercentageText = ProjectSampleDetail.AbsestosPercentageText;
                        updateProjectSample.CompositeNonAsbestosContents = ProjectSampleDetail.CompositeNonAsbestosContents;
                        updateProjectSample.CompositeNonAsbestosContentsText = ProjectSampleDetail.CompositeNonAsbestosContentsText;
                        updateProjectSample.DisplayOrder = ProjectSampleDetail.DisplayOrder;
                        updateProjectSample.IsBilable = ProjectSampleDetail.IsBilable;
                        updateProjectSample.CreatedOn = ProjectSampleDetail.CreatedOn;
                        updateProjectSample.CreatedBy = ProjectSampleDetail.CreatedBy;
                        updateProjectSample.UpdatedOn = ProjectSampleDetail.UpdatedOn;
                        updateProjectSample.UpdatedBy = ProjectSampleDetail.UpdatedBy;
                        updateProjectSample.IsAmendment = ProjectSampleDetail.IsAmendment;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ProjectSampleDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ProjectSampleDetail;
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
                    updateResponse.Result = ProjectSampleDetail;
                }
                return updateResponse;
            }
        }

        public Response<ProjectSampleDetail> Delete(long Id)
        {
            Response<ProjectSampleDetail> deleteResponse = new Response<ProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectSampleDetail deleteProjectSample = db.ProjectSampleDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteProjectSample != null)
                    {
                        db.ProjectSampleDetails.Remove(deleteProjectSample);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteProjectSample;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteProjectSample;
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
        public List<ProjectSampleDetailViewModel> GetAllProjectSampleDetailsBySampleId(long sampleId)
        {
            List<ProjectSampleDetailViewModel> projectViewModel = new List<ProjectSampleDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleDetailViewModel>("GLS_GetAllProjectSampleDetailsBySampleId @SampleId", sampleIdParameter).ToList<ProjectSampleDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleDetailViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<AmendmentProjectSampleDetailViewModel> GetAllAmendmentProjectSampleDetailsBySampleId(long sampleId)
        {
            List<AmendmentProjectSampleDetailViewModel> projectViewModel = new List<AmendmentProjectSampleDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleDetailViewModel>("GLS_GetAllAmendmentProjectSampleDetailsBySampleId @SampleId", sampleIdParameter).ToList<AmendmentProjectSampleDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleDetailViewModel>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
