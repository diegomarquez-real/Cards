using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Route("api/yugioh/[controller]")]
    [Produces("application/json")]
    public class SetsController : ControllerBase
    {
        private readonly ILogger<SetsController> _logger;
        private readonly Services.Yugioh.Abstractions.ISetService _setService;

        public SetsController(ILogger<SetsController> logger,
            Services.Yugioh.Abstractions.ISetService setService)
        {
            _logger = logger;
            _setService = setService;
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

                return BadRequest();
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

                return BadRequest();
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
                var set = await _setService.GetSetAsync(id);

                if (set == null)
                    return NotFound();

                await _setService.UpdateSetAsync(set, updateSetModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Set.");

                return BadRequest();
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

                return BadRequest();
            }
        }
    }
}