using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.FinancialModule;
using GLSPM.Data.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.FinancialModule
{
    public class IncomeStatementService
    {
        public List<IncomeStatementViewModel> GetIncomeStatementData(int month, int year)
        {
            List<IncomeStatementViewModel> IncomeStatementViewModel = new List<IncomeStatementViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var monthParameter = new SqlParameter { ParameterName = "Month", Value = month };
                var yearParameter = new SqlParameter { ParameterName = "Year", Value = year };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<IncomeStatementViewModel>("GLS_GetIncomeStatementData @Month,@Year", monthParameter, yearParameter).ToList<IncomeStatementViewModel>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<IncomeStatementViewModel>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<AttachmentLibraryViewModel> GetExpenceAttachment(int month, int year)
        {
            List<AttachmentLibraryViewModel> IncomeStatementViewModel = new List<AttachmentLibraryViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var monthParameter = new SqlParameter { ParameterName = "Month", Value = month };
                var yearParameter = new SqlParameter { ParameterName = "Year", Value = year };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<AttachmentLibraryViewModel>("GLS_GetExpenceAttachment @Month,@Year", monthParameter, yearParameter).ToList<AttachmentLibraryViewModel>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<AttachmentLibraryViewModel>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<StatementOfAccountsViewmodel> GetStatementOfAccounts(int month, int year, int clientid, string projectType)
        {
            List<StatementOfAccountsViewmodel> IncomeStatementViewModel = new List<StatementOfAccountsViewmodel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var monthParameter = new SqlParameter { ParameterName = "Month", Value = month };
                var yearParameter = new SqlParameter { ParameterName = "Year", Value = year };
                var clientidParameter = new SqlParameter { ParameterName = "ClientId", Value = clientid };
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<StatementOfAccountsViewmodel>("GLS_GetStatementOfAccounts @Month,@Year,@ClientId, @ProjectType", monthParameter, yearParameter, clientidParameter, projectTypeParameter).ToList<StatementOfAccountsViewmodel>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<StatementOfAccountsViewmodel>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<StatementOfAccountsViewmodel> GetStatementOfAccountsFromaDateRange(DateTime startDate, DateTime endDate, int clientid, string projectType)
        {
            List<StatementOfAccountsViewmodel> IncomeStatementViewModel = new List<StatementOfAccountsViewmodel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var startDateParameter = new SqlParameter { ParameterName = "startDate", Value = startDate };
                var endDateParameter = new SqlParameter { ParameterName = "endDate", Value = endDate };
                var clientidParameter = new SqlParameter { ParameterName = "ClientId", Value = clientid };
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<StatementOfAccountsViewmodel>("GLS_GetStatementOfAccountsFromaDateRange @startDate,@endDate,@ClientId, @ProjectType", startDateParameter, endDateParameter, clientidParameter, projectTypeParameter).ToList<StatementOfAccountsViewmodel>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<StatementOfAccountsViewmodel>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<StatementOfAccountsSummary> GetStatementOfAccountsSummary(DateTime startDate, DateTime endDate, int clientid, string projectType)
        {
            List<StatementOfAccountsSummary> IncomeStatementViewModel = new List<StatementOfAccountsSummary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var startDateParameter = new SqlParameter { ParameterName = "startDate", Value = startDate };
                var endDateParameter = new SqlParameter { ParameterName = "endDate", Value = endDate };
                var clientidParameter = new SqlParameter { ParameterName = "ClientId", Value = clientid };
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<StatementOfAccountsSummary>("GLS_GetStatementOfAccountsSummary @startDate,@endDate,@ClientId, @ProjectType", startDateParameter, endDateParameter, clientidParameter, projectTypeParameter).ToList<StatementOfAccountsSummary>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<StatementOfAccountsSummary>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<SampleCountSummaryForReportViewModel> GetSampleCountSummaryForReport(DateTime startDate, DateTime endDate, int clientid, string projectType)
        {
            List<SampleCountSummaryForReportViewModel> IncomeStatementViewModel = new List<SampleCountSummaryForReportViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var startDateParameter = new SqlParameter { ParameterName = "startDate", Value = startDate };
                var endDateParameter = new SqlParameter { ParameterName = "endDate", Value = endDate };
                var clientidParameter = new SqlParameter { ParameterName = "ClientId", Value = clientid };
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    IncomeStatementViewModel = db.Database.SqlQuery<SampleCountSummaryForReportViewModel>("GLS_SampleCountSummaryForReport @startDate,@endDate,@ClientId, @ProjectType", startDateParameter, endDateParameter, clientidParameter, projectTypeParameter).ToList<SampleCountSummaryForReportViewModel>();
                }
                catch (Exception ex) { }
                if (IncomeStatementViewModel == null)
                {
                    IncomeStatementViewModel = new List<SampleCountSummaryForReportViewModel>();
                }
                return IncomeStatementViewModel;
            }
        }
        public List<SampleSubmitSummary> GetSampleSubmitSummary(int month, int year, int clientid, int reportTo)
        {
            List<SampleSubmitSummary> sampleSubmitSummary = new List<SampleSubmitSummary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var monthParameter = new SqlParameter { ParameterName = "Month", Value = month };
                var yearParameter = new SqlParameter { ParameterName = "Year", Value = year };
                var clientidParameter = new SqlParameter { ParameterName = "ClientId", Value = clientid };
                var reportToParameter = new SqlParameter { ParameterName = "ReportTo", Value = reportTo };
                try
                {
                    sampleSubmitSummary = db.Database.SqlQuery<SampleSubmitSummary>("GLS_GetSampleSubmitSummary @Month,@Year,@ClientId, @ReportTo", monthParameter, yearParameter, clientidParameter, reportToParameter).ToList<SampleSubmitSummary>();
                }
                catch (Exception ex) { }
                if (sampleSubmitSummary == null)
                {
                    sampleSubmitSummary = new List<SampleSubmitSummary>();
                }
                return sampleSubmitSummary;
            }
        }
    }
}
