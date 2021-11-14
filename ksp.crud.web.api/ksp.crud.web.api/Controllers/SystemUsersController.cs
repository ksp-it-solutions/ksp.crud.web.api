using ksp.crud.web.api.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ksp.crud.web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUsersController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<SystemUsersController> _logger;

        public SystemUsersController(DataContext dbContext, ILogger<SystemUsersController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("{accesstoken}")]
        public async Task<IActionResult> GetSystemUsersAsync(string accesstoken)
        {
            // Validate token.
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            // Get information.
            _logger.LogInformation("Get a list of system users.");
            var systemusers = await _dbContext.Set<SystemUsers>().ToListAsync();
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            return Ok(systemusers);
        }

        [HttpPost("login/{accesstoken}")]
        public async Task<IActionResult> Login(string accesstoken, [FromBody] SystemUsers systemuser)
        {
            // Validate token.
            var token = await _dbContext.Set<Token>().FirstOrDefaultAsync(x => x.Value == accesstoken && x.IsExpirated == 0);
            if (token is null)
            {
                _logger.LogInformation($"Token indentify by { accesstoken } does not exist!");
                return BadRequest(new ErrorResponse() { Message = $"Token indentify by { accesstoken } does not exist!" });
            }
            _logger.LogInformation("Loggin user");
            var loggedsystemuser = await _dbContext.Set<SystemUsers>().FirstOrDefaultAsync(
                x => x.Password == systemuser.Password && 
                x.Username == systemuser.Username
            );
            if (loggedsystemuser is null)
            {
                _logger.LogInformation($"¡Login failed!\nUsername or password is wrong.");
                return NotFound(new ErrorResponse() { Message = $"¡Login failed!\nUsername or password is wrong." });
            }
            var employ = await _dbContext.Set<Employees>().FirstOrDefaultAsync(x => x.EmployId == loggedsystemuser.EmployId);
            loggedsystemuser.Employ = employ;
            // Set token as expirated
            _logger.LogInformation("Set token as expirated.");
            token.IsExpirated = 1;
            _dbContext.Set<Token>().Update(token);
            await _dbContext.SaveChangesAsync();
            return Ok(loggedsystemuser);
        }
    }
}
