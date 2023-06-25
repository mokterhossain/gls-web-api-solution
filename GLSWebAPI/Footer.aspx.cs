using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI
{
    public partial class Footer : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        public string labAnalystSignUrl = "";
        public string labrotaryManagerDiploma = "";
        public string projectAnalystDiploma = "";
        public string labrotaryManagerSignUrl = "";
        public string projectAnalystName = "";
        public string labrotaryManagerName = "";
        public string SampledBy = "";
        public string footerNoteText = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }

            project = new ProjectService().GetByProjectId2(projectId);
            labAnalystSignUrl = "http://192.168.23.10/gls/" + project.AnalystSignature.Replace("~/", "");

            labrotaryManagerSignUrl = "http://192.168.23.10/gls/" + project.LabratoryManagerSignature.Replace("~/", "");
            projectAnalystName = project.ProjectAnalystName;
            labrotaryManagerName = project.LabratoryManagerName;
            if (!string.IsNullOrEmpty(project.LabratoryManagerDiploma))
                labrotaryManagerDiploma = ", " + project.LabratoryManagerDiploma;
            if (!string.IsNullOrEmpty(project.ProjectAnalystDiploma))
                projectAnalystDiploma = ", " + project.ProjectAnalystDiploma;
            SampledBy = (!string.IsNullOrEmpty(project.SampledBy) ? "Sampled By: " + project.SampledBy : "");
            if (project.ProjectType == "PLM")
            {
                footerNoteText = "Test results relate to the samples, as submitted, to the Laboratory.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The report must not be used by the client to claim product clarification, approval, or authorization. by CALA, NIST, or any agency of the federal government.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This report shall not be reproduced, except in    full, without prior approval of the laboratory. In homogeneous sample are separated into homogeneous subsamples and analyze individually •When amounts of interfering materials indicate that the sample should be analyzed by gravimetric point count, the minimum detection and reporting limit will be less than 1%, unless point counting is performed.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;MMVF - Man made vitrious fiber i.e glass fiber, MMOF- Man made organic fiber, • NPAF- Natural Polymer and animal fiber i.e Hair, Mica-Micaceous Material.";
            }
            else if (project.ProjectType == "PCM")
            {
                footerNoteText = "• Interpretation is left to the company and/or persons who conducted the field work.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;• Field blank, if submitted with the project, has been used to correct the data. • Reporting limit is calculated using a minimum detection limit of 7 fibers / mm2. • Upper 95 % Confidence Limit, calculated using a relative standard deviation value of 0.40. • A \"Version\" greater than 1 indicates amended data.";
            }
            else if (project.ProjectType == "Mold")
            {
                footerNoteText = "Particulate matter on the slide like dust in the air are graded from 1 to 5 indicating the largest amounts. When no trace is indicated, the count is completed but this can be an indication of an error during the sample collection Total Spore / m3 has been rounded to two significant figures to analytical Precision.";
            }
            else if (project.ProjectType == "Mold Tape Lift")
            {
                footerNoteText = "*The guideline has been used by the microscopist for reporting the concentration is as follows: ‘Loaded’ = 80% or greater, ‘many’ = 31% to 79 %, ‘few’ = 30 % or less.";
            }
        }
    }
}