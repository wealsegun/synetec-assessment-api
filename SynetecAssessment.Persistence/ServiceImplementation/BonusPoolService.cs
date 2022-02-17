﻿using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.DTO;
using SynetecAssessmentApi.Persistence.LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly AppDbContext _dbContext;

        public BonusPoolService()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");

            _dbContext = new AppDbContext(dbContextOptionBuilder.Options);
            
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        EmployeeId = employee.Id,
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Id = employee.DepartmentId,
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            //Check If Employee details exist
            if (employee != null)
            {
                //get the total salary budget for the company
                int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

                //calculate the bonus allocation for the employee
                decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
                int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);
                return new BonusPoolCalculatorResultDto
                {
                    Employee = new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        EmployeeId = employee.Id,
                        Department = new DepartmentDto
                        {
                            Id = employee.DepartmentId,
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    },

                    Amount = bonusAllocation
                };
            }
            else
            {
                return null;
            }



        }
    }
}
