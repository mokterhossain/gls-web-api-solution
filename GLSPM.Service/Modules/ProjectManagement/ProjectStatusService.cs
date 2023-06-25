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
    public class ProjectStatusService
    {
        #region Common Methods

        public List<ProjectStatus> All()
        {
            List<ProjectStatus> ProjectStatus = new List<ProjectStatus>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectStatus = db.ProjectStatuss.OrderBy(f => f.StatusId).AsParallel().ToList();

                if (ProjectStatus == null)
                {
                    ProjectStatus = new List<ProjectStatus>();
                }
                return ProjectStatus;
            }
        }

        public ProjectStatus GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectStatus ProjectStatus = db.ProjectStatuss.SingleOrDefault(u => u.StatusId == userRoleId);
                return ProjectStatus;
            }
        }
        public Response<ProjectStatus> Add(ProjectStatus ProjectStatus)
        {
            Response<ProjectStatus> addResponse = new Response<ProjectStatus>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ProjectStatuss.Add(ProjectStatus);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ProjectStatus;
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
                    addResponse.Result = ProjectStatus;
                }
                return addResponse;
            }
        }

        public Response<ProjectStatus> Update(ProjectStatus ProjectStatus)
        {
            Response<ProjectStatus> updateResponse = new Response<ProjectStatus>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectStatus updateProjectStatus = db.ProjectStatuss.SingleOrDefault(u => u.StatusId == ProjectStatus.StatusId);
                    if (updateProjectStatus != null)
                    {
                        //updateProjectStatus = userRole;
                        updateProjectStatus.StatusId = ProjectStatus.StatusId;
                        updateProjectStatus.StatusName = ProjectStatus.StatusName;
                        updateProjectStatus.SerialNo = ProjectStatus.SerialNo;
                        updateProjectStatus.IsActive = ProjectStatus.IsActive;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ProjectStatus;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ProjectStatus;
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
                    updateResponse.Result = ProjectStatus;
                }
                return updateResponse;
            }
        }

        public Response<ProjectStatus> Delete(long StatusId)
        {
            Response<ProjectStatus> deleteResponse = new Response<ProjectStatus>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectStatus deleteProjectStatus = db.ProjectStatuss.SingleOrDefault(u => u.StatusId == StatusId);
                    if (deleteProjectStatus != null)
                    {
                        db.ProjectStatuss.Remove(deleteProjectStatus);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteProjectStatus;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteProjectStatus;
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
