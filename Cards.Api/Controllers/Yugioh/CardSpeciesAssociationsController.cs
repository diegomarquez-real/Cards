using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class CardSpeciesAssociationsController : ControllerBase
    {
        private readonly ILogger<CardSpeciesAssociationsController> _logger;
        private readonly Services.Yugioh.Abstractions.ICardSpeciesAssociationService _cardSpeciesAssociationService;

        public CardSpeciesAssociationsController(ILogger<CardSpeciesAssociationsController> logger,
            Services.Yugioh.Abstractions.ICardSpeciesAssociationService cardSpeciesAssociationService)
        {
            _logger = logger;
            _cardSpeciesAssociationService = cardSpeciesAssociationService;
        }

        [HttpGet("{id}", Name = "GetCardSpeciesAssociation")]
        [ProducesResponseType(typeof(Models.Yugioh.CardSpeciesAssociationModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCardSpeciesAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                var cardSpeciesAssociation = await _cardSpeciesAssociationService.GetCardSpeciesAssociationAsync(id);

                if (cardSpeciesAssociation == null)
                    return NotFound();

                return Ok(cardSpeciesAssociation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get CardSpeciesAssociation.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateCardSpeciesAssociation")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateCardSpeciesAssociationAsync([FromBody] Models.Yugioh.Create.CreateCardSpeciesAssociationModel createCardSpeciesAssociationModel)
        {
            try
            {
                var result = await _cardSpeciesAssociationService.CreateCardSpeciesAssociationAsync(createCardSpeciesAssociationModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create CardSpeciesAssociation.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteCardSpeciesAssociation")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteCardSpeciesAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                await _cardSpeciesAssociationService.DeleteCardSpeciesAssociationAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete CardSpeciesAssociation.");

                return BadRequest();
            }
        }
    }
}
