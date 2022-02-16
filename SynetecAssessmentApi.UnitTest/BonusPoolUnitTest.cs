using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.DTO;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Persistence.LogicInterface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.UnitTest
{
    [TestClass]
    public class BonusPoolUnitTest
    {
        [TestMethod]
        public  void GetEmployeesAsync()
        {
            
           var _servce = new BonusPoolService();           
            var employee = new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4)

            };


            var actual = employee;
            var expected = Task.Run(()=> _servce.GetEmployeesAsync().Result);


            Assert.AreEqual(expected.Result.FirstOrDefault()?.Fullname, actual.FirstOrDefault()?.Fullname);
        }

        [TestMethod]
        public void CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            bonusPoolAmount = 123456;
            selectedEmployeeId = 1;
            var _servce = new BonusPoolService();
            BonusPoolCalculatorResultDto actual = new BonusPoolCalculatorResultDto
            {
                Amount = 11313,
                Employee = new EmployeeDto
                {
                    EmployeeId = 1,
                    Department = new DepartmentDto
                    {
                        Id = 0,
                        Title = "Finance",
                        Description = "The finance department for the company"
                    },
                    Fullname = "John Smith",
                    JobTitle = "Accountant (Senior)",
                    Salary = 60000
                }
            };

            var expected = Task.Run(() => _servce.CalculateAsync(bonusPoolAmount, selectedEmployeeId)).Result;


            Assert.AreEqual(expected.Amount, actual.Amount);


        }
    }
}

