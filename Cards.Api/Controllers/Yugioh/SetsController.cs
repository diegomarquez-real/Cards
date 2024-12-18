using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class SetsController : ControllerBase
    {
        private readonly ILogger<SetsController> _logger;
        private readonly Services.Yugioh.Abstractions.ISetService _setService;
        private readonly Data.Abstractions.Repositories.Yugioh.ISetRepository _setRepository;

        public SetsController(ILogger<SetsController> logger,
            Services.Yugioh.Abstractions.ISetService setService,
            Data.Abstractions.Repositories.Yugioh.ISetRepository setRepository)
        {
            _logger = logger;
            _setService = setService;
            _setRepository = setRepository;
        }

        [HttpGet("{id}", Name = "GetSet")]
        [ProducesResponseType(typeof(Models.Yugioh.SetModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetSetAsync([FromRoute] Guid id)
        {
            try
            {
                var set = await _setService.GetSetAsync(id);

                if (set == null)
                    return NotFound();

                return Ok(set);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Set.");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}", Name = "GetSetByName")]
        [ProducesResponseType(typeof(Models.Yugioh.SetModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public async Task<IActionResult> GetSetByNameAsync([FromRoute] string name)
        {
            try
            {
                var set = await _setService.GetSetByNameAsync(name);

                if (set == null)
                    return NoContent();

                return Ok(set);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Set.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CreateSet")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateSetAsync([FromBody] Models.Yugioh.Create.CreateSetModel createSetModel)
        {
            try
            {
                var result = await _setService.CreateSetAsync(createSetModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Set.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateSet")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateSetAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdateSetModel updateSetModel)
        {
            try
            {
                var set = await _setRepository.FindByIdAsync(id);

                if (set == null)
                    return NotFound();

                await _setService.UpdateSetAsync(set, updateSetModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Set.");

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteSet")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteSetAsync([FromRoute] Guid id)
        {
            try
            {
                await _setService.DeleteSetAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Set.");

                return BadRequest(ex.Message);
            }
        }
    }
}