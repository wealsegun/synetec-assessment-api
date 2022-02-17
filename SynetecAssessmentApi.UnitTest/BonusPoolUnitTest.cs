using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Domain.DTO;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Persistence.LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.UnitTest
{

    [TestClass]
    public class BonusPoolUnitTest
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private AppDbContext _dbContext;
        private IBonusPoolService _bonus { get; set; }

        public IServiceProvider _serviceProv { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            Services = new ServiceCollection();

            Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "HrDb"),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);
            _bonus = new BonusPoolService();

            ServiceProvider = Services.BuildServiceProvider();

            var scope = ServiceProvider.CreateScope();
            var _serviceProf = scope.ServiceProvider;

            DbContextGenerator.Initialize(_serviceProf);
        }
        [TestMethod]
        public void GetEmployeesAsync()
        {
            //var _servce = new BonusPoolService();


            var employee = new List<Employee>
                {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4)
                };

            var actual = employee;
            var expected = Task.Run(() => _bonus.GetEmployeesAsync().Result);


            Assert.AreEqual(expected.Result.FirstOrDefault()?.Fullname, actual.FirstOrDefault()?.Fullname);

        }

        [TestMethod]
        public void CalculateAsync()
        {
            int bonusPoolAmount = 123456;
            int selectedEmployeeId = 1;
            //var _servce = new BonusPoolService();
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

            var expected = Task.Run(() => _bonus.CalculateAsync(bonusPoolAmount, selectedEmployeeId));

            Assert.AreEqual(expected.Result.Amount, actual.Amount);


        }
    }



    // This method gets called by the runtime. Use this method to add services to the container.
   
}

