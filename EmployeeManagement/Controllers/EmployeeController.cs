using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller])")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeemngContext db;

        public EmployeeController(EmployeemngContext context)
        {
            db = context;
        }
        [HttpGet]
        //[Route("api/Employee/GetEmployeeList")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeList()
        {
            var data = await db.Employees.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        //[Route("api/Employee/GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var emp = await db.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(emp);
            }
        }

        [HttpPost]
        //[Route("api/Employee/CreateEmployee")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            await db.Employees.AddAsync(emp);
            await db.SaveChangesAsync();

            return Ok(emp);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> PutEmployee(int id, Employee emp)
        {
            if(id != emp.Id)
            {
                return BadRequest();
            }
            db.Entry(emp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!EmpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await db.Employees.FindAsync(id);
            if(emp == null)
            {
                return NotFound();
            }
            db.Employees.Remove(emp);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpExists(int id)
        {
            return db.Employees.Any(e => e.Id == id);
        }

        [HttpGet("GetSimilarLocationOfEmployees")]
        public async Task<List<Employee>> GetSimilarLocationOfEmployees(string? loc)
        {
            return await db.Employees.Where(l => l.Address == loc).ToListAsync();
        }
    }
}
