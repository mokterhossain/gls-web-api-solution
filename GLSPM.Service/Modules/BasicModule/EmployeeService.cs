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
    public class EmployeeService
    {
        #region Common Methods

        public List<Employee> All()
        {
            List<Employee> Employee = new List<Employee>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Employee = db.Employees.OrderBy(f => f.EmployeeId).AsParallel().ToList();

                if (Employee == null)
                {
                    Employee = new List<Employee>();
                }
                return Employee;
            }
        }

        public Employee GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Employee Employee = db.Employees.SingleOrDefault(u => u.EmployeeId == userRoleId);
                return Employee;
            }
        }
        public Employee GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Employee Employee = db.Employees.SingleOrDefault(u => u.Name == name);
                return Employee;
            }
        }
        public Response<Employee> Add(Employee Employee)
        {
            Response<Employee> addResponse = new Response<Employee>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Employees.Add(Employee);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Employee;
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
                    addResponse.Result = Employee;
                }
                return addResponse;
            }
        }

        public Response<Employee> Update(Employee Employee)
        {
            Response<Employee> updateResponse = new Response<Employee>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Employee updateEmployee = db.Employees.SingleOrDefault(u => u.EmployeeId == Employee.EmployeeId);
                    if (updateEmployee != null)
                    {
                        //updateEmployee = userRole;
                        updateEmployee.EmployeeId = Employee.EmployeeId;
                        updateEmployee.Name = Employee.Name;
                        updateEmployee.Designation = Employee.Designation;
                        updateEmployee.CellNo = Employee.CellNo;
                        updateEmployee.Email = Employee.Email;
                        updateEmployee.OfficePhone = Employee.OfficePhone;
                        updateEmployee.IsAnalyst = Employee.IsAnalyst;
                        
                        updateEmployee.IsLabrotaryManager = Employee.IsLabrotaryManager;
                        updateEmployee.IsQao = Employee.IsQao;
                        updateEmployee.SignatureUrl = Employee.SignatureUrl;
                        updateEmployee.IsActive = Employee.IsActive;
                        updateEmployee.Diploma = Employee.Diploma;
                        updateEmployee.CanSignOnInvoice = Employee.CanSignOnInvoice;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = Employee;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = Employee;
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
                    updateResponse.Result = Employee;
                }
                return updateResponse;
            }
        }

        public Response<Employee> Delete(long Id)
        {
            Response<Employee> deleteResponse = new Response<Employee>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Employee deleteEmployee = db.Employees.SingleOrDefault(u => u.EmployeeId == Id);
                    if (deleteEmployee != null)
                    {
                        db.Employees.Remove(deleteEmployee);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteEmployee;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteEmployee;
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
