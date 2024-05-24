using EmployeeIncomeManagement.Data;
using EmployeeIncomeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EmployeeIncomeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBContext dbContext;
        public EmployeeController(EmployeeDBContext dBContext) {
            this.dbContext = dBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel viewModel)

        {
            

            if (ModelState.IsValid) {
                Int64 income = Convert.ToInt64(viewModel.TotalIncome);
                Int64 totalGross =0;

                if (income < 250000)
                {
                    totalGross = income;
                }

                if (income >250001 && income < 500000)
                {
                   Int64 gross = income / 5;
                    totalGross = income - gross;
                }
               
                    if ( 500001 < income && income < 750000)
                    {
                    Int64 gross = income / 10;
                    totalGross = income - gross;
                }
                if (750001 < income  && income < 1000000)
                {
                    Int64 gross = income / 15;
                    totalGross = income - gross;
                }
                if (1000001 < income  && income < 1250000)
                {
                    Int64 gross = income / 20;
                    totalGross = income - gross;
                }
                if (1250001 < income  && income < 1500000)
                {
                    Int64 gross = income / 25;
                    totalGross = income - gross;
                }
                if (income >1500001)
                {
                    Int64 gross = income / 30;
                    totalGross = income - gross;
                }




                var employee = new Employee
                {
                    Email = viewModel.Email,
                    Name = viewModel.Name,
                    TotalIncome = Convert.ToString(totalGross),
                };
               await dbContext.Employees.AddAsync(employee);
               await dbContext.SaveChangesAsync();
                return RedirectToAction("GetEmployee");
                    
 
            }
            return View(viewModel);
        }
    }
}
