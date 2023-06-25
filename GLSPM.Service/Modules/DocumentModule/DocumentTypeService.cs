using GLSPM.Data;
using GLSPM.Data.Modules.DocumentModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.DocumentModule
{
    public class DocumentTypeService
    {
        #region Common Methods

        public List<DocumentType> All()
        {
            List<DocumentType> DocumentType = new List<DocumentType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentType = db.DocumentTypes.OrderBy(f => f.Id).AsParallel().ToList();

                if (DocumentType == null)
                {
                    DocumentType = new List<DocumentType>();
                }
                return DocumentType;
            }
        }

        public DocumentType GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentType DocumentType = db.DocumentTypes.SingleOrDefault(u => u.Id == userRoleId);
                return DocumentType;
            }
        }
        public Response<DocumentType> Add(DocumentType DocumentType)
        {
            Response<DocumentType> addResponse = new Response<DocumentType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.DocumentTypes.Add(DocumentType);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = DocumentType;
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
                    addResponse.Result = DocumentType;
                }
                return addResponse;
            }
        }

        public Response<DocumentType> Update(DocumentType DocumentType)
        {
            Response<DocumentType> updateResponse = new Response<DocumentType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentType updateDocumentType = db.DocumentTypes.SingleOrDefault(u => u.Id == DocumentType.Id);
                    if (updateDocumentType != null)
                    {
                        //updateDocumentType = userRole;
                        updateDocumentType.Id = DocumentType.Id;
                        updateDocumentType.TypeName = DocumentType.TypeName;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = DocumentType;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = DocumentType;
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
                    updateResponse.Result = DocumentType;
                }
                return updateResponse;
            }
        }

        public Response<DocumentType> Delete(long Id)
        {
            Response<DocumentType> deleteResponse = new Response<DocumentType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentType deleteDocumentType = db.DocumentTypes.SingleOrDefault(u => u.Id == Id);
                    if (deleteDocumentType != null)
                    {
                        db.DocumentTypes.Remove(deleteDocumentType);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteDocumentType;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteDocumentType;
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
