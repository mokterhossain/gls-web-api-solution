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
    public class LocationService
    {
        #region Common Methods

        public List<Location> GetSomeLocation()
        {
            List<Location> Location = new List<Location>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Location = db.Locations.Take(50).OrderBy(f => f.Id).AsParallel().ToList();

                if (Location == null)
                {
                    Location = new List<Location>();
                }
                return Location;
            }
        }
        public List<Location> All()
        {
            List<Location> Location = new List<Location>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Location = db.Locations.OrderBy(f => f.Id).AsParallel().ToList();

                if (Location == null)
                {
                    Location = new List<Location>();
                }
                return Location;
            }
        }
        public List<TestLocation> All2()
        {
            List<TestLocation> Location = new List<TestLocation>();

            using (GLSPMContext db = new GLSPMContext())
            {
                var list = db.Locations.Select(st => new { Id = st.Id, LocationName = st.LocationName });

                Location = list.Select(l => new TestLocation
                {
                   LocationName=  l.LocationName
                }).ToList();
                // Location = db.Locations.OrderBy(f => f.Id).AsParallel().ToList();

                if (Location == null)
                {
                    Location = new List<TestLocation>();
                }
                return Location;
            }
        }
        
        public List<Location> AllByLocationName(string location)
        {
            List<Location> Location = new List<Location>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Location = db.Locations.Where(l=>l.LocationName == location).OrderBy(f => f.Id).AsParallel().ToList();

                if (Location == null)
                {
                    Location = new List<Location>();
                }
                return Location;
            }
        }
        public Location GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Location Location = db.Locations.SingleOrDefault(u => u.Id == userRoleId);
                return Location;
            }
        }
        public Response<Location> Add(Location Location)
        {
            Response<Location> addResponse = new Response<Location>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Locations.Add(Location);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Location;
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
                    addResponse.Result = Location;
                }
                return addResponse;
            }
        }

        public Response<Location> Update(Location Location)
        {
            Response<Location> updateResponse = new Response<Location>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Location updateLocation = db.Locations.SingleOrDefault(u => u.Id == Location.Id);
                    if (updateLocation != null)
                    {
                        //updateLocation = userRole;
                        updateLocation.Id = Location.Id;
                        updateLocation.LocationName = Location.LocationName;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = Location;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = Location;
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
                    updateResponse.Result = Location;
                }
                return updateResponse;
            }
        }

        public Response<Location> Delete(long Id)
        {
            Response<Location> deleteResponse = new Response<Location>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Location deleteLocation = db.Locations.SingleOrDefault(u => u.Id == Id);
                    if (deleteLocation != null)
                    {
                        db.Locations.Remove(deleteLocation);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteLocation;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteLocation;
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
