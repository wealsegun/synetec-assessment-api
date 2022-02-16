using System;
using System.Collections.Generic;
using System.Text;

namespace SynetecAssessmentApi.Domain.DTO
{
   public class CalculateBonusDto
    {
        public int TotalBonusPoolAmount { get; set; }
        public int SelectedEmployeeId { get; set; }
    }
}
