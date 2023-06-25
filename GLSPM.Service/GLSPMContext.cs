using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.DocumentModule;
using GLSPM.Data.Modules.FinancialModule;
using GLSPM.Data.Modules.InventoryModule;
using GLSPM.Data.Modules.InvoiceModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Data.Modules.UserSettings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service
{
    public class GLSPMContext : DbContext
    {
        static GLSPMContext()
        {
            Database.SetInitializer<GLSPMContext>(null);
        }
        public GLSPMContext(): base("GLSPMConnection")
        {
            this.SetCommandTimeOut(300);
        }
        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        #region HR
        #endregion

        #region User Setttings
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserLoginHistoryLog> UserLoginHistoryLogs { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        #endregion

        #region Client
        public DbSet<ClientInvoice> ClientInvoices { get; set; }
        public DbSet<ClientInvoiceDetail> ClientInvoiceDetails { get; set; }
        public DbSet<InvoiceSetting> InvoiceSettings { get; set; }
        #endregion

        #region Project
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSample> ProjectSamples { get; set; }
        public DbSet<ProjectSampleDetail> ProjectSampleDetails { get; set; }
        public DbSet<BatchNumberRecord> BatchNumberRecords { get; set; }
        public DbSet<ProjectStatus> ProjectStatuss { get; set; }

        public DbSet<PCM> PCMs { get; set; }
        public DbSet<QCAnalyticData> QCAnalyticDatas { get; set; }

        public DbSet<PCMFieldBlankRawData> PCMFieldBlankRawDatas { get; set; }

        public DbSet<AmendmentProject> AmendmentProjects { get; set; }
        public DbSet<AmendmentProjectSample> AmendmentProjectSamples { get; set; }
        public DbSet<AmendmentProjectSampleDetail> AmendmentProjectSampleDetails { get; set; }

        public DbSet<AmendmentPCM> AmendmentPCMs { get; set; }

        public DbSet<AmendmentPCMFieldBlankRawData> AmendmentPCMFieldBlankRawDatas { get; set; }

        public DbSet<AmendmentPCMCV> AmendmentPCMCVs { get; set; }
        public DbSet<MenuDefinition> MenuDefinitions { get; set; }
        public DbSet<RolesMenu> RolesMenus { get; set; }

        public DbSet<MoldSporeType> MoldSporeTypes { get; set; }
        public DbSet<Mold> Molds { get; set; }
        public  DbSet<MoldSample> MoldSamples { get; set; }
        public DbSet<MoldSampleDetail> MoldSampleDetails { get; set; }
        public DbSet<MoldSetting> MoldSettings { get; set; }

        public DbSet<MoldTapeLiftSampleDetail> MoldTapeLiftSampleDetails { get; set; }
        #endregion
        #region Basic Module
        public DbSet<Client> Clients { get; set; }
        public DbSet<SampleLayer> SampleLayers { get; set; }
        public DbSet<SampleType> SampleTypes { get; set; }
        public DbSet<SampleCompositeHomogenity> SampleCompositeHomogenitys { get; set; }
        public DbSet<QC> QCs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AsbestosPercentage> AsbestosPercentages { get; set; }
         
        public DbSet<AsbestosPercentageDetail> AsbestosPercentageDetails { get; set; }

        public DbSet<CompositeNonAsbestosContentsDetail> CompositeNonAsbestosContentsDetails { get; set; }

        public DbSet<CompositeNonAsbestosContents> CompositeNonAsbestosContentss { get; set; }
        public DbSet<ClientContactPerson> ClientContactPersons { get; set; }
        public DbSet<Matrix> Matrixs { get; set; }
        public DbSet<PackageCode> PackageCodes { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<NonConformanceReport> NonConformanceReports { get; set; }

        public DbSet<AttachmentLibrary> AttachmentLibrarys { get; set; }
        public DbSet<SampledBy> SampledBys { get; set; }
        public DbSet<PCMCV> PCMCVs { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<EmailAccounts> EmailAccountss { get; set; }
        public DbSet<EmailHistory> EmailHistorys { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }

        public DbSet<EmailSchedule> EmailSchedules { get; set; }
        public DbSet<PCMComment> PCMComments { get; set; }
        #endregion
        #region Inventory Module
        public DbSet<InventoryEquipment> InventoryEquipments { get; set; }
        public DbSet<InventoryChemical> InventoryChemicals { get; set; }
        public DbSet<InventoryAttachment> InventoryAttachments { get; set; }
        public DbSet<InventoryOfficeSupply> InventoryOfficeSupplys { get; set; }
        public DbSet<InventoryGeneralLabSupply> InventoryGeneralLabSupplys { get; set; }
        public DbSet<InventorySupplyType> InventorySupplyTypes { get; set; }
        public DbSet<InventoryChemicalName> InventoryChemicalNames { get; set; }
        public DbSet<InventoryChemicalDetail> InventoryChemicalDetails { get; set; }

        public DbSet<InventoryEquipmentDetail> InventoryEquipmentDetails { get; set; }
        #endregion
        #region Finacial Module
        public DbSet<ExpenseTransaction> ExpenseTransactions { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        //public DbSet<IncomeStatementViewModel> IncomeStatements { get; set; }
        #endregion
        #region Document Module
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentLibrary> DocumentLibrarys { get; set; }
        public DbSet<DocumentFolderStructure> DocumentFolderStructures { get; set; }
        #endregion
    }
}
