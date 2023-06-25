using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.BasicModule
{
    public class QCService
    {
        #region Common Methods

        public List<QC> All()
        {
            List<QC> QC = new List<QC>();

            using (GLSPMContext db = new GLSPMContext())
            {
                QC = db.QCs.OrderBy(f => f.Id).AsParallel().ToList();

                if (QC == null)
                {
                    QC = new List<QC>();
                }
                return QC;
            }
        }

        public QC GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                QC QC = db.QCs.SingleOrDefault(u => u.Id == userRoleId);
                return QC;
            }
        }
        public Response<QC> Add(QC QC)
        {
            Response<QC> addResponse = new Response<QC>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.QCs.Add(QC);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = QC;
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
                    addResponse.Result = QC;
                }
                return addResponse;
            }
        }

        public Response<QC> Update(QC QC)
        {
            Response<QC> updateResponse = new Response<QC>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    QC updateQC = db.QCs.SingleOrDefault(u => u.Id == QC.Id);
                    if (updateQC != null)
                    {
                        //updateQC = userRole;
                        updateQC.Id = QC.Id;
                        updateQC.Name = QC.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = QC;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = QC;
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
                    updateResponse.Result = QC;
                }
                return updateResponse;
            }
        }

        public Response<QC> Delete(long Id)
        {
            Response<QC> deleteResponse = new Response<QC>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    QC deleteQC = db.QCs.SingleOrDefault(u => u.Id == Id);
                    if (deleteQC != null)
                    {
                        db.QCs.Remove(deleteQC);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteQC;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteQC;
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
        public List<InvoiceDetails> GetInvoiceDetailsTeamExor()
        {
            List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();
            InvoiceDetails id = new InvoiceDetails();
            id.Description = "<div style=\"padding:10px;\"><div></div><div>1. Add sampling date to the report</div><div>2. Misc. changes on reports</div></div>";
            id.TotalHour = 30;
            id.Rate = (decimal)30.00;
            id.Total = 500;
            invoiceDetails.Add(id);

            return invoiceDetails;
        }
        public List<InvoiceDetails> GetInvoiceDetailsTeamExor2()
        {
            List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();
            InvoiceDetails id = new InvoiceDetails();
            id.Description = "<div style=\"padding:10px;\"><div></div><div>1. Reinstall the system & Setup new server</div><div>2. Restore Data from old hard disk</div><div>3. New implementation as per requirements</div></div>";
            id.TotalHour = 160;
            id.Rate = (decimal)12.50;
            id.Total = 20000;
            invoiceDetails.Add(id);

            return invoiceDetails;
        }
    }
}
