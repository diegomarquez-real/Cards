using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Authorize]
    [Route("api/yugioh/[controller]")]
    [ApiExplorerSettings(GroupName = "Yugioh")]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly Services.Yugioh.Abstractions.ICardService _cardService;
        private readonly Data.Abstractions.Repositories.Yugioh.ICardRepository _cardRepository;

        public CardsController(ILogger<CardsController> logger,
            Services.Yugioh.Abstractions.ICardService cardService,
            Data.Abstractions.Repositories.Yugioh.ICardRepository cardRepository)
        {
            _logger = logger;
            _cardService = cardService;
            _cardRepository = cardRepository;
        }

        [HttpGet("{id}", Name = "GetCard")]
        [ProducesResponseType(typeof(Models.Yugioh.CardModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCardAsync([FromRoute] Guid id)
        {
            try
            {
                var card = await _cardService.GetCardAsync(id);

                if (card == null)
                    return NotFound();

                return Ok(card);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Card.");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetAllOrQuery")]
        [ProducesResponseType(typeof(List<Models.Yugioh.CardModel>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> GetAllOrQueryAsync([FromQuery] Models.Yugioh.Query.CardQueryModel cardQueryModel)
        {
            try
            {
                var cards = await _cardService.GetAllOrQueryAsync(cardQueryModel);

                return Ok(cards);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Card.");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}", Name = "GetCardByName")]
        [ProducesResponseType(typeof(Models.Yugioh.CardModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public async Task<IActionResult> GetCardByNameAsync([FromRoute] string name)
        {
            try
            {
                var card = await _cardService.GetCardByNameAsync(name);

                if (card == null)
                    return NoContent();

                return Ok(card);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Card.");

                return BadRequest(ex.Message);
            }
        }



        [HttpPost(Name = "CreateCard")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateCardAsync([FromBody] Models.Yugioh.Create.CreateCardModel createCardModel)
        {
            try
            {
                var result = await _cardService.CreateCardAsync(createCardModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Card.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateCard")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateCardAsync([FromRoute] Guid id, [FromBody] Models.Yugioh.Update.UpdateCardModel updateCardModel)
        {
            try
            {
                var card = await _cardRepository.FindByIdAsync(id);

                if (card == null)
                    return NotFound();

                await _cardService.UpdateCardAsync(card, updateCardModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Card.");

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteCard")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteCardAsync([FromRoute] Guid id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Card.");

                return BadRequest(ex.Message);
            }
        }
    }
}