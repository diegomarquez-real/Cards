using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly Services.Abstractions.ICardService _cardService;

        public CardsController(Services.Abstractions.ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("{id}", Name = "GetCard")]
        [ProducesResponseType(typeof(Models.CardModel), 200)]
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
                //_logger.LogError(ex, "Failed To Get Card.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateCard")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateCardAsync([FromBody] Models.Create.CreateCardModel createCardModel)
        {
            try
            {
                var result = await _cardService.CreateCardAsync(createCardModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed To Create Card.");

                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateCard")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateCardAsync([FromRoute] Guid id, [FromBody] Models.Update.UpdateCardModel updateCardModel)
        {
            try
            {
                var card = await _cardService.GetCardAsync(id);

                if (card == null)
                    return NotFound();

                await _cardService.UpdateCardAsync(card, updateCardModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed To Update Card.");

                return BadRequest();
            }
        }
    }
}