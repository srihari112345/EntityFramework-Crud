using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityWebApi.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        [HttpPost]
        public ActionResult InsertDetails([FromBody]EmployeeModel model)
        {
            using (var dbcontext = new EmployeeDbContext())
            {
                var employeeModel = new EmployeeModel();
                employeeModel.Name = model.Name;
                employeeModel.Email = model.Email;
                employeeModel.DateOfJoining = DateTime.Now;
                dbcontext.Employee.Add(employeeModel);
                dbcontext.SaveChanges();

            }
            return Ok("One Row Inserted");  
        }
   
        public ActionResult GetDetails()
        {
            DbSet<EmployeeModel> details;
            using (var dbcontext = new EmployeeDbContext())
            {
                details = dbcontext.Employee;
                return Ok(details.ToList());
            }
                
        }

        [HttpDelete]
        public ActionResult DeleteEmployees([FromBody]EmployeeModel model)
        {
            using (var dbContext = new EmployeeDbContext())
            {
                var deleteRowById = (from employeeTable in dbContext.Employee
                              where employeeTable.Id == model.Id
                              select employeeTable).Single();
                dbContext.Remove(deleteRowById);
                dbContext.SaveChanges();
            }
                return Ok("Deleted");
        }
    }
}
