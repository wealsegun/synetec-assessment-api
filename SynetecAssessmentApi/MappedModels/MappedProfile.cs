using AutoMapper;
using SynetecAssessmentApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.MappedModels
{
    public class MappedProfile: Profile
    {
        public MappedProfile()
        {
            // Parse Model or Input Model
            CreateMap<CalculateBonusParseModel, CalculateBonusDto>();


            // View Models or Output Models
            CreateMap<BonusPoolCalculatorResultDto, BonusPoolCalculatorResultViewModel>();
            CreateMap<EmployeeDto, EmployeeViewModel>();
            CreateMap<DepartmentDto, DepartmentViewModel>();



        }
    }
}
