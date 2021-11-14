using ksp.crud.web.api.DataModel;
using ksp.crud.web.api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ksp.crud.web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<TokenController> _logger;

        public TokenController(DataContext dbContext, ILogger<TokenController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken()
        {
            _logger.LogInformation("Creation of token value.");
            Token token = new Token();
            token.Value = HelperToken.GenerateToken(10);
            _dbContext.Set<Token>().Add(token);
            await _dbContext.SaveChangesAsync();
            return Ok(token);
        }
    }
}
