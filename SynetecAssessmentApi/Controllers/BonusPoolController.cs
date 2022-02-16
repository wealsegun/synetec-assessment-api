using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Domain.DTO;
using SynetecAssessmentApi.Persistence.LogicInterface;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPool;
        public BonusPoolController(IBonusPoolService bonusPool)
        {
            _bonusPool = bonusPool;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Response = await Task.Run(() => _bonusPool.GetEmployeesAsync());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Response = await Task.Run(() => _bonusPool.CalculateAsync(request.TotalBonusPoolAmount,
               request.SelectedEmployeeId));
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
