using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class AttributesController : ControllerBase
    {
        private readonly ILogger<AttributesController> _logger;
        private readonly Services.Yugioh.Abstractions.IAttributeService _attributeService;
        private readonly Data.Abstractions.Repositories.Yugioh.IAttributeRepository _attributeRepository;

        public AttributesController(ILogger<AttributesController> logger,
            Services.Yugioh.Abstractions.IAttributeService attributeService,
            Data.Abstractions.Repositories.Yugioh.IAttributeRepository attributeRepository)
        {
            _logger = logger;
            _attributeService = attributeService;
            _attributeRepository = attributeRepository;
        }

        [HttpGet("{id}", Name = "GetAttribute")]
        [ProducesResponseType(typeof(Models.Yugioh.AttributeModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetAttributeAsync([FromRoute] Guid id)
        {
            try
            {
                var attribute = await _attributeService.GetAttributeAsync(id);

                if (attribute == null)
                    return NotFound();

                return Ok(attribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Attribute.");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}", Name = "GetAttributeByName")]
        [ProducesResponseType(typeof(Models.Yugioh.AttributeModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetAttributeByNameAsync([FromRoute] string name)
        {
            try
            {
                var attribute = await _attributeService.GetAttributeByNameAsync(name);

                if (attribute == null)
                    return NotFound();

                return Ok(attribute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Attribute.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CreateAttribute")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateAttributeAsync([FromBody] Models.Yugioh.Create.CreateAttributeModel createAttributeModel)
        {
            try
            {
                var result = await _attributeService.CreateAttributeAsync(createAttributeModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Attribute.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateAttribute")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateAttributeAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdateAttributeModel updateAttributeModel)
        {
            try
            {
                var attribute = await _attributeRepository.FindByIdAsync(id);

                if (attribute == null)
                    return NotFound();

                await _attributeService.UpdateAttributeAsync(attribute, updateAttributeModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Attribute.");

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteAttribute")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteAttributeAsync([FromRoute] Guid id)
        {
            try
            {
                await _attributeService.DeleteAttributeAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Attribute.");

                return BadRequest(ex.Message);
            }
        }
    }
}
