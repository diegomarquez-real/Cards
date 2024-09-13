using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class CardSetAssociationsController : ControllerBase
    {
        private readonly ILogger<CardSetAssociationsController> _logger;
        private readonly Services.Yugioh.Abstractions.ICardSetAssociationService _cardSetAssociationService;

        public CardSetAssociationsController(ILogger<CardSetAssociationsController> logger,
            Services.Yugioh.Abstractions.ICardSetAssociationService cardSetAssociationService)
        {
            _logger = logger;
            _cardSetAssociationService = cardSetAssociationService;
        }

        [HttpGet("{id}", Name = "GetCardSetAssociation")]
        [ProducesResponseType(typeof(Models.Yugioh.CardSetAssociationModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCardSetAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                var cardSetAssociation = await _cardSetAssociationService.GetCardSetAssociationAsync(id);

                if (cardSetAssociation == null)
                    return NotFound();

                return Ok(cardSetAssociation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get CardSetAssociation.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateCardSetAssociation")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateCardSetAssociationAsync([FromBody] Models.Yugioh.Create.CreateCardSetAssociationModel createCardSetAssociationModel)
        {
            try
            {
                var result = await _cardSetAssociationService.CreateCardSetAssociationAsync(createCardSetAssociationModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create CardSetAssociation.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteCardSetAssociation")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteCardSetAssociationAsync([FromRoute] Guid id)
        {
            try
            {
                await _cardSetAssociationService.DeleteCardSetAssociationAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete CardSetAssociation.");

                return BadRequest();
            }
        }
    }
}