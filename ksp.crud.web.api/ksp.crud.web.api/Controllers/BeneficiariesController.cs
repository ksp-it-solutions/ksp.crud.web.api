using ksp.crud.web.api.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<BeneficiariesController> _logger;

        public BeneficiariesController(DataContext dbContext, ILogger<BeneficiariesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("{accesstoken}/{id}")]
        public async Task<IActionResult> GetBeneficiary(string accesstoken, Guid id)
        {
            // Validate token
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Get information.
            _logger.LogInformation("Get beneficiery.");
            var beneficiary = await _dbContext.Set<Beneficiaries>().FirstOrDefaultAsync(x => x.BeneficiaryId == id);
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            return Ok(beneficiary);
        }

        [HttpGet("employ/{accesstoken}/{id}")]
        public async Task<IActionResult> GetBeneficiariesForEmploy(string accesstoken, Guid id)
        {
            // Validate token
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Get information.
            _logger.LogInformation("Get beneficiaries.");
            var beneficiary = await _dbContext.Set<Beneficiaries>().Where(x => x.EmployId == id).ToListAsync();
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            return Ok(beneficiary);
        }

        [HttpPost("{accesstoken}")]
        public async Task<IActionResult> CreateBeneficiary(string accesstoken, [FromBody] Beneficiaries beneficiary)
        {
            // Validate token
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            // Create Employ and Systemuser records.
            if (beneficiary is null)
            {
                _logger.LogInformation("Beneficiary sent from client is null");
                return BadRequest("Beneficiary is null");
            }
            _dbContext.Set<Beneficiaries>().Add(beneficiary);
            var dto = await _dbContext.SaveChangesAsync();
            return Ok(beneficiary);
        }

        [HttpPut("{accesstoken}/{id}")]
        public async Task<IActionResult> UpdateBeneficiary(string accesstoken, Guid id, [FromBody] Beneficiaries beneficiary)
        {
            // Validate token.
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Validate request.
            if (beneficiary is null)
            {
                _logger.LogInformation("Beneficiary sent from client is null");
                return BadRequest(new ErrorResponse() { Message = $"Beneficiary is null" });
            }
            // Update and validate beneficiary record.
            var recordBeneficiary = await _dbContext.Set<Beneficiaries>().FirstOrDefaultAsync(x => x.BeneficiaryId == id);
            if (recordBeneficiary is null)
            {
                _logger.LogInformation($"Beneficiary indentify by { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Beneficiary indentify by { id } does not exist!" });
            }
            _dbContext.Set<Beneficiaries>().Update(beneficiary);
            var dto = await _dbContext.SaveChangesAsync();
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{accesstoken}/{id}")]
        public async Task<IActionResult> DeleteBeneficiaryById(string accesstoken, Guid id)
        {
            // Validate token
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            // Get information
            _logger.LogInformation($"Getting a Beneficiary identify by {id}");
            var beneficiary = await _dbContext.Set<Beneficiaries>().FirstOrDefaultAsync(x => x.BeneficiaryId == id);
            if (beneficiary is null)
            {
                _logger.LogInformation($"Beneficiary indentify by { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Beneficiary does not exist!" });
            }
            _dbContext.Set<Beneficiaries>().Remove(beneficiary);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
