using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Domain.DTO;
using SynetecAssessmentApi.MappedModels;
using SynetecAssessmentApi.Persistence.LogicInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPool;
        private readonly IMapper _mapper;
        public BonusPoolController(IBonusPoolService bonusPool, IMapper mapper)
        {
            _bonusPool = bonusPool;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Response = _mapper.Map<List<EmployeeViewModel>>( await Task.Run(() => _bonusPool.GetEmployeesAsync()));
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusParseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = _mapper.Map<CalculateBonusDto>(model);
                    var Response =_mapper.Map<BonusPoolCalculatorResultViewModel>( await Task.Run(() => _bonusPool.CalculateAsync(request.TotalBonusPoolAmount,
               request.SelectedEmployeeId)));
                    if (Response != null)
                    {
                        return Ok(Response);
                    }
                    else
                    {
                        return BadRequest("The employee is not found.");
                    }                    
                }
                else
                {
                    return BadRequest("Invalid Parameters");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
