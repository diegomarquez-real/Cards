using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class CardEffectTypeAssociationsController : ControllerBase
    {
        private readonly ILogger<CardEffectTypeAssociationsController> _logger;
        private readonly Services.Yugioh.Abstractions.ICardEffectTypeAssociationService _cardEffectTypeAssociationService;

        public CardEffectTypeAssociationsController(ILogger<CardEffectTypeAssociationsController> logger,
            Services.Yugioh.Abstractions.ICardEffectTypeAssociationService cardEffectTypeAssociationService)
        {
            _logger = logger;
            _cardEffectTypeAssociationService = cardEffectTypeAssociationService;
        }

        [HttpGet("{id}", Name = "GetCardEffectTypeAssociation")]
        [ProducesResponseType(typeof(Models.Yugioh.CardEffectTypeAssociationModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCardEffectTypeAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                var cardEffectTypeAssociation = await _cardEffectTypeAssociationService.GetCardEffectTypeAssociationAsync(id);

                if (cardEffectTypeAssociation == null)
                    return NotFound();

                return Ok(cardEffectTypeAssociation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get CardEffectTypeAssociation.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateCardEffectTypeAssociation")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateCardEffectTypeAssociationAsync([FromBody] Models.Yugioh.Create.CreateCardEffectTypeAssociationModel createCardEffectTypeAssociationModel)
        {
            try
            {
                var result = await _cardEffectTypeAssociationService.CreateCardEffectTypeAssociationAsync(createCardEffectTypeAssociationModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create CardEffectTypeAssociation.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteCardEffectTypeAssociation")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteCardEffectTypeAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                await _cardEffectTypeAssociationService.DeleteCardEffectTypeAssociationAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete CardEffectTypeAssociation.");

                return BadRequest();
            }
        }
    }
}