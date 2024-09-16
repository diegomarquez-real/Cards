using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class PowersController : ControllerBase
    {
        private readonly ILogger<PowersController> _logger;
        private readonly Services.Yugioh.Abstractions.IPowerService _powerService;
        private readonly Data.Abstractions.Repositories.Yugioh.IPowerRepository _powerRepository;

        public PowersController(ILogger<PowersController> logger,
            Services.Yugioh.Abstractions.IPowerService powerService,
            Data.Abstractions.Repositories.Yugioh.IPowerRepository powerRepository)
        {
            _logger = logger;
            _powerService = powerService;
            _powerRepository = powerRepository;
        }

        [HttpGet("{id}", Name = "GetPower")]
        [ProducesResponseType(typeof(Models.Yugioh.PowerModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetPowerAsync([FromRoute] Guid id)
        {
            try
            {
                var power = await _powerService.GetPowerAsync(id);

                if (power == null)
                    return NotFound();

                return Ok(power);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Power.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreatePower")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreatePowerAsync([FromBody] Models.Yugioh.Create.CreatePowerModel createPowerModel)
        {
            try
            {
                var result = await _powerService.CreatePowerAsync(createPowerModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Power.");

                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdatePower")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdatePowerAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdatePowerModel updatePowerModel)
        {
            try
            {
                var power = await _powerRepository.FindByIdAsync(id);

                if (power == null)
                    return NotFound();

                await _powerService.UpdatePowerAsync(power, updatePowerModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Power.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeletePower")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeletePowerAsync([FromRoute] Guid id)
        {
            try
            {
                await _powerService.DeletePowerAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Power.");

                return BadRequest();
            }
        }
    }
}