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
    public class RolesMenuService
    {
        #region Common Methods

        public List<RolesMenu> All()
        {
            List<RolesMenu> RolesMenu = new List<RolesMenu>();

            using (GLSPMContext db = new GLSPMContext())
            {
                RolesMenu = db.RolesMenus.OrderBy(f => f.Id).AsParallel().ToList();

                if (RolesMenu == null)
                {
                    RolesMenu = new List<RolesMenu>();
                }
                return RolesMenu;
            }
        }

        public RolesMenu GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                RolesMenu RolesMenu = db.RolesMenus.SingleOrDefault(u => u.Id == userRoleId);
                return RolesMenu;
            }
        }
        public Response<RolesMenu> Add(RolesMenu RolesMenu)
        {
            Response<RolesMenu> addResponse = new Response<RolesMenu>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.RolesMenus.Add(RolesMenu);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = RolesMenu;
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
                    addResponse.Result = RolesMenu;
                }
                return addResponse;
            }
        }

        public Response<RolesMenu> Update(RolesMenu RolesMenu)
        {
            Response<RolesMenu> updateResponse = new Response<RolesMenu>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    RolesMenu updateRolesMenu = db.RolesMenus.SingleOrDefault(u => u.Id == RolesMenu.Id);
                    if (updateRolesMenu != null)
                    {
                        //updateRolesMenu = userRole;
                        updateRolesMenu.Id = RolesMenu.Id;
                        updateRolesMenu.RoleID = RolesMenu.RoleID;
                        updateRolesMenu.MenuID = RolesMenu.MenuID;
                        updateRolesMenu.Attribute1 = RolesMenu.Attribute1;
                        updateRolesMenu.Attribute2 = RolesMenu.Attribute2;
                        updateRolesMenu.Attribute3 = RolesMenu.Attribute3;
                        updateRolesMenu.Attribute4 = RolesMenu.Attribute4;
                        updateRolesMenu.Attribute5 = RolesMenu.Attribute5;
                        updateRolesMenu.Attribute6 = RolesMenu.Attribute6;
                        updateRolesMenu.Attribute7 = RolesMenu.Attribute7;
                        updateRolesMenu.Attribute8 = RolesMenu.Attribute8;
                        updateRolesMenu.Attribute9 = RolesMenu.Attribute9;
                        updateRolesMenu.Attribute10 = RolesMenu.Attribute10;
                        updateRolesMenu.CanEdit = RolesMenu.CanEdit;
                        updateRolesMenu.CanAdd = RolesMenu.CanAdd;
                        updateRolesMenu.CanDelete = RolesMenu.CanDelete;
                        updateRolesMenu.ViewOnly = RolesMenu.ViewOnly;
                        updateRolesMenu.CreatedBy = RolesMenu.CreatedBy;
                        updateRolesMenu.CreatedOn = RolesMenu.CreatedOn;
                        updateRolesMenu.UpdatedBy = RolesMenu.UpdatedBy;
                        updateRolesMenu.UpdatedOn = RolesMenu.UpdatedOn;


                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = RolesMenu;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = RolesMenu;
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
                    updateResponse.Result = RolesMenu;
                }
                return updateResponse;
            }
        }

        public Response<RolesMenu> Delete(long Id)
        {
            Response<RolesMenu> deleteResponse = new Response<RolesMenu>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    RolesMenu deleteRolesMenu = db.RolesMenus.SingleOrDefault(u => u.Id == Id);
                    if (deleteRolesMenu != null)
                    {
                        db.RolesMenus.Remove(deleteRolesMenu);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteRolesMenu;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteRolesMenu;
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
        public Response<RolesMenu> DeleteRolesMenu(int RoleId, int MenuId)
        {
            Response<RolesMenu> deleteResponse = new Response<RolesMenu>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    RolesMenu deleteRolesMenu = db.RolesMenus.SingleOrDefault(u => u.RoleID == RoleId && u.MenuID == MenuId);
                    if (deleteRolesMenu != null)
                    {
                        db.RolesMenus.Remove(deleteRolesMenu);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteRolesMenu;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteRolesMenu;
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
        public List<RolesMenuViewModel> GetRolesMenus(int RoleID, int MenuID)
        {
            List<RolesMenuViewModel> rolesMenu = new List<RolesMenuViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var RoleIDParameter = new SqlParameter { ParameterName = "RoleID", Value = RoleID };
                var MenuIDParameter = new SqlParameter { ParameterName = "MenuID", Value = MenuID };
                try
                {
                    rolesMenu = db.Database.SqlQuery<RolesMenuViewModel>("GLS_RolesMenu_GetRolesMenus @RoleID,@MenuID", RoleIDParameter, MenuIDParameter).ToList<RolesMenuViewModel>();
                }
                catch (Exception ex) { }
                if (rolesMenu == null)
                {
                    rolesMenu = new List<RolesMenuViewModel>();
                }
                return rolesMenu;
            }
        }
        public RolesMenu GetRolesMenuByID(int RoleID, int MenuID)
        {
            List<RolesMenu> rolesMenu = new List<RolesMenu>();
            RolesMenu rm = new RolesMenu();
            using (GLSPMContext db = new GLSPMContext())
            {
                var RoleIDParameter = new SqlParameter { ParameterName = "RoleID", Value = RoleID };
                var MenuIDParameter = new SqlParameter { ParameterName = "MenuID", Value = MenuID };
                try
                {
                    rolesMenu = db.Database.SqlQuery<RolesMenu>("GLS_RolesMenu_GetByID @RoleID,@MenuID", RoleIDParameter, MenuIDParameter).ToList<RolesMenu>();
                }
                catch (Exception ex) { }
                if (rolesMenu != null)
                {
                    rm = rolesMenu.FirstOrDefault();
                }
                return rm;
            }
        }
        #endregion Common Methods
    }
}
