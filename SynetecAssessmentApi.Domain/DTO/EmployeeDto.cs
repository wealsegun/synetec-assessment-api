using System;
using System.Collections.Generic;
using System.Text;

namespace SynetecAssessmentApi.Domain.DTO
{
   public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
