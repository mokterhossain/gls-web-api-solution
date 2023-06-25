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
    public class MenuDefinitionService
    {
        public List<MenuDefinition> All()
        {
            List<MenuDefinition> menuDefinitionList = new List<MenuDefinition>();

            using (GLSPMContext db = new GLSPMContext())
            {
                menuDefinitionList = db.MenuDefinitions.OrderBy(f => f.Tier).AsParallel().ToList();

                if (menuDefinitionList == null)
                {
                    menuDefinitionList = new List<MenuDefinition>();
                }
                return menuDefinitionList;
            }
        }
        public MenuDefinition GetByMenuDefinitionId(int menuDefinitionId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MenuDefinition menuDefinition = db.MenuDefinitions.SingleOrDefault(u => u.MenuDefinitionID == menuDefinitionId);
                return menuDefinition;
            }
        }
        public Response<MenuDefinition> Add(MenuDefinition menuDefinition)
        {
            Response<MenuDefinition> addResponse = new Response<MenuDefinition>();
            using (GLSPMContext db = new GLSPMContext())
            {

                db.MenuDefinitions.Add(menuDefinition);
                try
                {
                    int checkSuccess = 0;

                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add success";
                        addResponse.IsSuccess = true;
                        addResponse.Result = menuDefinition;
                    }

                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This MenuDefinition Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the MenuDefinition Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = menuDefinition;
                }
                return addResponse;
            }
        }

        public Response<MenuDefinition> Update(MenuDefinition menuDefinition)
        {
            Response<MenuDefinition> updateResponse = new Response<MenuDefinition>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MenuDefinition updateMenuDefinition = db.MenuDefinitions.SingleOrDefault(u => u.MenuDefinitionID == menuDefinition.MenuDefinitionID);
                    if (updateMenuDefinition != null)
                    {
                        updateMenuDefinition.MenuDefinitionID = menuDefinition.MenuDefinitionID;
                        updateMenuDefinition.Title = menuDefinition.Title;
                        updateMenuDefinition.URL = menuDefinition.URL;
                        updateMenuDefinition.OrderNumber = menuDefinition.OrderNumber;
                        updateMenuDefinition.Tier = menuDefinition.Tier;
                        updateMenuDefinition.UpdatedOn = DateTime.Now;
                        updateMenuDefinition.UpdatedBy = menuDefinition.UpdatedBy;



                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = menuDefinition;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = menuDefinition;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = "MenuDefinition Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This MenuDefinition Info already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the MenuDefinition Info: " + ex.Message;
                    }
                    updateResponse.Result = menuDefinition;
                }

                return updateResponse;
            }
        }

        public Response<MenuDefinition> Delete(int menuDefinitionId)
        {
            Response<MenuDefinition> deleteResponse = new Response<MenuDefinition>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MenuDefinition deleteMenuDefinition = db.MenuDefinitions.SingleOrDefault(u => u.MenuDefinitionID == menuDefinitionId);
                    if (deleteMenuDefinition != null)
                    {
                        //deleteSystemManu.RecordStatus = false;
                        db.MenuDefinitions.Remove(deleteMenuDefinition);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMenuDefinition;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "MenuDefinition Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMenuDefinition;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = "MenuDefinition Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This MenuDefinition Info currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting MenuDefinition Info: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }

                return deleteResponse;
            }
        }
    }
}
