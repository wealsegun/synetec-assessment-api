using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.MappedModels
{
    public class EmployeeViewModel
    {
        public long EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentViewModel Department { get; set; }
    }
}
