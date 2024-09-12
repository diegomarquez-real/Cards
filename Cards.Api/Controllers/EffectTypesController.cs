using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EffectTypesController : ControllerBase
    {
        private readonly ILogger<EffectTypesController> _logger;
        private readonly Services.Abstractions.IEffectTypeService _effectTypeService;

        public EffectTypesController(ILogger<EffectTypesController> logger,
            Services.Abstractions.IEffectTypeService effectTypeService)
        {
            _logger = logger;
            _effectTypeService = effectTypeService;
        }

        [HttpGet("{id}", Name = "GetEffectType")]
        [ProducesResponseType(typeof(Models.EffectTypeModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetEffectTypeAsync([FromRoute] Guid id)
        {
            try
            {
                var effectType = await _effectTypeService.GetEffectTypeAsync(id);

                if (effectType == null)
                    return NotFound();

                return Ok(effectType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get EffectType.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateEffectType")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateEffectTypeAsync([FromBody] Models.Create.CreateEffectTypeModel createEffectTypeModel)
        {
            try
            {
                var result = await _effectTypeService.CreateEffectTypeAsync(createEffectTypeModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create EffectType.");

                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateEffectType")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateEffectTypeAsync([FromRoute] Guid id, [FromBody] Models.Update.UpdateEffectTypeModel updateEffectTypeModel)
        {
            try
            {
                var effectType = await _effectTypeService.GetEffectTypeAsync(id);

                if (effectType == null)
                    return NotFound();

                await _effectTypeService.UpdateEffectTypeAsync(effectType, updateEffectTypeModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update EffectType.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteEffectType")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteEffectTypeAsync([FromRoute] Guid id)
        {
            try
            {
                await _effectTypeService.DeleteEffectTypeAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete EffectType.");

                return BadRequest();
            }
        }
    }
}
