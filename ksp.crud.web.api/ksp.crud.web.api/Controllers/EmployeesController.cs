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
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(DataContext dbContext, ILogger<EmployeesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("{accesstoken}")]
        public async Task<IActionResult> GetEmployees(string accesstoken,[FromQuery] Guid ignoreId)
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
            // Get information.
            _logger.LogInformation("Get a list of employees.");
            var employees = await _dbContext.Set<Employees>().Where(x => x.EmployId != ignoreId).ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{accesstoken}/{id}")]
        public async Task<IActionResult> GetEmployById(string accesstoken, Guid id)
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
            _logger.LogInformation($"Getting a Employ identify by {id}");
            var employ = await _dbContext.Set<Employees>().FirstOrDefaultAsync(x => x.EmployId == id);
            if(employ is null)
            {
                _logger.LogInformation($"Employ indentify by { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Employ does not exist!" });
            }
            var systemuser = await _dbContext.Set<SystemUsers>().FirstOrDefaultAsync(x => x.EmployId == id);
            if (systemuser is null)
            {
                _logger.LogInformation($"System user indentify by employ id { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Employ does not exist!" });
            }
            systemuser.Employ = employ;
            return Ok(systemuser);
        }

        [HttpPost("{accesstoken}")]
        public async Task<IActionResult> CreateEmploy(string accesstoken, [FromBody] SystemUsers systemUser)
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
            if (systemUser is null)
            {
                _logger.LogInformation("Employ sent from client is null");
                return BadRequest(new ErrorResponse() { Message = $"Employ is null" });
            }
            var existenceuser = await _dbContext.Set<SystemUsers>().FirstOrDefaultAsync(x => x.Username == systemUser.Username);
            if (existenceuser is not null)
            {
                _logger.LogInformation("¡Username already exists!");
                return BadRequest(new ErrorResponse() { Message = "¡Username already exists!" });
            }
            _dbContext.Set<Employees>().Add(systemUser.Employ);
            _dbContext.Set<SystemUsers>().Add(systemUser);
            await _dbContext.SaveChangesAsync();
            return Ok(systemUser);
        }

        [HttpPut("{accesstoken}/{id}")]
        public async Task<IActionResult> UpdateEmploy(string accesstoken, Guid id, [FromBody] SystemUsers systemUser)
        {
            // Validate token.
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
            // Update Employ and Systemuser records.
            if (systemUser is null)
            {
                _logger.LogInformation("Employ sent from client is null");
                return BadRequest("Employ is null");
            }
            // Validate employ.
            var employ = await _dbContext.Set<Employees>().FirstOrDefaultAsync(x => x.EmployId == id);
            if (employ is null)
            {
                _logger.LogInformation($"Employ indentify by { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Employ indentify by { id } does not exist!" });
            }
            var existenceuser = await _dbContext.Set<SystemUsers>().FirstOrDefaultAsync(x => x.Username == systemUser.Username && x.EmployId != id);
            if (existenceuser is not null)
            {
                _logger.LogInformation("¡Username already exists!");
                return BadRequest(new ErrorResponse() { Message = "¡Username already exists!" });
            }
            _dbContext.Set<Employees>().Update(systemUser.Employ);
            _dbContext.Set<SystemUsers>().Update(systemUser);
            var dto = await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{accesstoken}/{id}")]
        public async Task<IActionResult> DeleteEmployById(string accesstoken, Guid id)
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
            _logger.LogInformation($"Getting a Employ identify by {id}");
            var employ = await _dbContext.Set<Employees>().FirstOrDefaultAsync(x => x.EmployId == id);
            if (employ is null)
            {
                _logger.LogInformation($"Employ indentify by { id } does not exist!");
                return NotFound(new ErrorResponse() { Message = $"Employ does not exist!" });
            }
            var systemuser = await _dbContext.Set<SystemUsers>().FirstOrDefaultAsync(x => x.EmployId == id);
            if(systemuser is not null)
                _dbContext.Set<SystemUsers>().Remove(systemuser);
            _dbContext.Set<Employees>().Remove(employ);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
