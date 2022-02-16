using SynetecAssessmentApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.LogicInterface
{
    public interface IBonusPoolService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}
