using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class SpeciesController : ControllerBase
    {
        private readonly ILogger<SpeciesController> _logger;
        private readonly Services.Yugioh.Abstractions.ISpeciesService _speciesService;

        public SpeciesController(ILogger<SpeciesController> logger,
            Services.Yugioh.Abstractions.ISpeciesService speciesService)
        {
            _logger = logger;
            _speciesService = speciesService;
        }

        [HttpGet("{id}", Name = "GetSpecies")]
        [ProducesResponseType(typeof(Models.Yugioh.SpeciesModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetSpeciesAsync([FromRoute] Guid id)
        {
            try
            {
                var species = await _speciesService.GetSpeciesAsync(id);

                if (species == null)
                    return NotFound();

                return Ok(species);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Species.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateSpecies")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateSpeciesAsync([FromBody] Models.Yugioh.Create.CreateSpeciesModel createSpeciesModel)
        {
            try
            {
                var result = await _speciesService.CreateSpeciesAsync(createSpeciesModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Species.");

                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateSpecies")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateSpeciesAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdateSpeciesModel updateSpeciesModel)
        {
            try
            {
                var species = await _speciesService.GetSpeciesAsync(id);

                if (species == null)
                    return NotFound();

                await _speciesService.UpdateSpeciesAsync(species, updateSpeciesModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Species.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteSpecies")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteSpeciesAsync([FromRoute] Guid id)
        {
            try
            {
                await _speciesService.DeleteSpeciesAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Species.");

                return BadRequest();
            }
        }
    }
}