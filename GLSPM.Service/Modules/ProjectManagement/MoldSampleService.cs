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
    public class MoldSampleService
    {
        #region Common Methods

        public List<MoldSample> All()
        {
            List<MoldSample> MoldSample = new List<MoldSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample = db.MoldSamples.OrderBy(f => f.Id).AsParallel().ToList();

                if (MoldSample == null)
                {
                    MoldSample = new List<MoldSample>();
                }
                return MoldSample;
            }
        }
        public List<MoldSample> AllByMoldId(long MoldId)
        {
            List<MoldSample> MoldSample = new List<MoldSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample = db.MoldSamples.Where(s=>s.MoldId == MoldId).OrderBy(f => f.Id).AsParallel().ToList();

                if (MoldSample == null)
                {
                    MoldSample = new List<MoldSample>();
                }
                return MoldSample;
            }
        }
        public MoldSample GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample MoldSample = db.MoldSamples.SingleOrDefault(u => u.Id == userRoleId);
                return MoldSample;
            }
        }
        public MoldSample GetBySampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample MoldSample = db.MoldSamples.SingleOrDefault(u => u.SampleId == sampleId);
                return MoldSample;
            }
        }
        public MoldSample GetBySampleID(long sampleId, bool isQC)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample MoldSample = db.MoldSamples.SingleOrDefault(u => u.SampleId == sampleId && (u.IsQC == null ? false : u.IsQC) == isQC);
                return MoldSample;
            }
        }
        public MoldSample GetBySampleIDAndSerial(long sampleId, double serial)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample MoldSample = db.MoldSamples.FirstOrDefault(u => u.SampleId == sampleId && u.SerialNo == serial && (u.IsDuplicate == null ? false : u.IsDuplicate) == false);
                return MoldSample;
            }
        }
        public MoldSample GetBySampleIDAndSerial2(long sampleId, double serial)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSample MoldSample = db.MoldSamples.FirstOrDefault(u => u.SampleId == sampleId && u.SerialNo == serial && u.IsDuplicate == true);
                return MoldSample;
            }
        }
        public Response<MoldSample> Add(MoldSample MoldSample)
        {
            Response<MoldSample> addResponse = new Response<MoldSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.MoldSamples.Add(MoldSample);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = MoldSample;
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
                    addResponse.Result = MoldSample;
                }
                return addResponse;
            }
        }

        public Response<MoldSample> Update(MoldSample MoldSample)
        {
            Response<MoldSample> updateResponse = new Response<MoldSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSample updateMoldSample = db.MoldSamples.SingleOrDefault(u => u.Id == MoldSample.Id);
                    if (updateMoldSample != null)
                    {
                        //updateMoldSample = userRole;
                        updateMoldSample.Id = MoldSample.Id;
                        updateMoldSample.MoldId = MoldSample.MoldId;
                        updateMoldSample.SampleId = MoldSample.SampleId;
                        updateMoldSample.Volume = MoldSample.Volume;
                        updateMoldSample.Location = MoldSample.Location;
                        updateMoldSample.LabId = MoldSample.LabId;
                        updateMoldSample.AdditionalInformation = MoldSample.AdditionalInformation;
                        updateMoldSample.BackgroundDebries = MoldSample.BackgroundDebries;
                        updateMoldSample.AnalysisDate = MoldSample.AnalysisDate;
                        updateMoldSample.CommentsIndex = MoldSample.CommentsIndex;
                        updateMoldSample.Overloaded = MoldSample.Overloaded;
                        updateMoldSample.IsQC = MoldSample.IsQC;
                        updateMoldSample.IsDuplicate = MoldSample.IsDuplicate;
                        updateMoldSample.SerialNo = MoldSample.SerialNo;
                        updateMoldSample.UpdatedOn = DateTime.Now;
                        // updateMoldSample.MoldSampleDetails = MoldSample.MoldSampleDetails;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = MoldSample;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = MoldSample;
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
                    updateResponse.Result = MoldSample;
                }
                return updateResponse;
            }
        }

        public Response<MoldSample> Delete(long Id)
        {
            Response<MoldSample> deleteResponse = new Response<MoldSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSample deleteMoldSample = db.MoldSamples.SingleOrDefault(u => u.Id == Id);
                    if (deleteMoldSample != null)
                    {
                        db.MoldSamples.Remove(deleteMoldSample);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMoldSample;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMoldSample;
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
        public List<MoldSampleViewModel> GetMoldSample(long projectId)
        {
            List<MoldSampleViewModel> projectViewModel = new List<MoldSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldSampleViewModel>("GLS_GetMoldSample @ProjectId", projectIdParameter).ToList<MoldSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        #endregion

    }
}
