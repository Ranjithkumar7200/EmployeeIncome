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
        public EmployeeController(EmployeeDBContext dBContext)
        {
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
            if (ModelState.IsValid)
            {
                Int64 income = Convert.ToInt64(viewModel.TotalIncome);
                Int64 totalGross = 0;
                if (income <= 250000)
                {
                    totalGross = income;
                }
                else if (income <= 500000)
                {
                    Int64 gross = income / 5; // 20% tax
                    totalGross = income - gross;
                }
                else if (income <= 750000)
                {
                    Int64 gross = income / 10; // 10% tax
                    totalGross = income - gross;
                }
                else if (income <= 1000000)
                {
                    Int64 gross = income / 15; // Approx 6.67% tax
                    totalGross = income - gross;
                }
                else if (income <= 1250000)
                {
                    Int64 gross = income / 20; // 5% tax
                    totalGross = income - gross;
                }
                else if (income <= 1500000)
                {
                    Int64 gross = income / 25; // 4% tax
                    totalGross = income - gross;
                }
                else
                {
                    Int64 gross = income / 30; // Approx 3.33% tax
                    totalGross = income - gross;
                }
                var employee = new Employee
                {
                    Email = viewModel.Email,
                    Name = viewModel.Name,
                    TotalIncome = totalGross.ToString(), // Convert to string
                };
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("GetEmployee");
            }
            return View(viewModel);
        }
    }
}