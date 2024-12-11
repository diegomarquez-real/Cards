using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class EffectTypesController : ControllerBase
    {
        private readonly ILogger<EffectTypesController> _logger;
        private readonly Services.Yugioh.Abstractions.IEffectTypeService _effectTypeService;
        private readonly Data.Abstractions.Repositories.Yugioh.IEffectTypeRepository _effectTypeRepository;

        public EffectTypesController(ILogger<EffectTypesController> logger,
            Services.Yugioh.Abstractions.IEffectTypeService effectTypeService,
            Data.Abstractions.Repositories.Yugioh.IEffectTypeRepository effectTypeRepository)
        {
            _logger = logger;
            _effectTypeService = effectTypeService;
            _effectTypeRepository = effectTypeRepository;
        }

        [HttpGet("{id}", Name = "GetEffectType")]
        [ProducesResponseType(typeof(Models.Yugioh.EffectTypeModel), 200)]
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

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}", Name = "GetEffectTypeByName")]
        [ProducesResponseType(typeof(Models.Yugioh.EffectTypeModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetEffectTypeByNameAsync([FromRoute] string name)
        {
            try
            {
                var effectType = await _effectTypeService.GetEffectTypeByNameAsync(name);

                if (effectType == null)
                    return NotFound();

                return Ok(effectType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get EffectType.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CreateEffectType")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateEffectTypeAsync([FromBody] Models.Yugioh.Create.CreateEffectTypeModel createEffectTypeModel)
        {
            try
            {
                var result = await _effectTypeService.CreateEffectTypeAsync(createEffectTypeModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create EffectType.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateEffectType")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateEffectTypeAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdateEffectTypeModel updateEffectTypeModel)
        {
            try
            {
                var effectType = await _effectTypeRepository.FindByIdAsync(id);

                if (effectType == null)
                    return NotFound();

                await _effectTypeService.UpdateEffectTypeAsync(effectType, updateEffectTypeModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update EffectType.");

                return BadRequest(ex.Message);
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

                return BadRequest(ex.Message);
            }
        }
    }
}
