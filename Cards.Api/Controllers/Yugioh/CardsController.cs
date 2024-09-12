﻿using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Yugioh
{
    [ApiController]
    [Route("api/yugioh/[controller]")]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly Services.Yugioh.Abstractions.ICardService _cardService;

        public CardsController(ILogger<CardsController> logger,
            Services.Yugioh.Abstractions.ICardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
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

                return BadRequest();
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

                return BadRequest();
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
                var card = await _cardService.GetCardAsync(id);

                if (card == null)
                    return NotFound();

                await _cardService.UpdateCardAsync(card, updateCardModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Card.");

                return BadRequest();
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

                return BadRequest();
            }
        }
    }
}