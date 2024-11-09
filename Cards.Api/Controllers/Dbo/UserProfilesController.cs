using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Api.Controllers.Dbo
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Dbo")]
    [Produces("application/json")]
    public class UserProfilesController : ControllerBase
    {
        private readonly ILogger<UserProfilesController> _logger;
        private readonly Services.Dbo.Abstractions.IUserProfileService _userProfileService;
        private readonly Data.Abstractions.Repositories.Dbo.IUserProfileRepository _userProfileRepository;
        private readonly Services.Identity.Abstractions.IUserAuthenticationService _userAuthenticationService;

        public UserProfilesController(ILogger<UserProfilesController> logger,
            Services.Dbo.Abstractions.IUserProfileService userProfileService,
            Data.Abstractions.Repositories.Dbo.IUserProfileRepository userProfileRepository,
            Services.Identity.Abstractions.IUserAuthenticationService userAuthenticationService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
            _userProfileRepository = userProfileRepository;
            _userAuthenticationService = userAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(Models.Identity.AuthTokenModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public async Task<IActionResult> Authenticate([FromBody] Models.Identity.UserProfileLoginModel userProfileLoginModel)
        {
            try
            {
                var authTokenModel = await _userAuthenticationService.AuthenticateAsync(userProfileLoginModel);

                if (authTokenModel == null)
                    return Unauthorized();

                return Ok(authTokenModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Authenticate.");

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUserProfile")]
        [ProducesResponseType(typeof(Models.Dbo.UserProfileModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetUserProfileAsync([FromRoute] Guid id)
        {
            try
            {
                var userProfile = await _userProfileService.GetUserProfileAsync(id);

                if (userProfile == null)
                    return NotFound();

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get UserProfile.");

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost(Name = "CreateUserProfile")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateUserProfileAsync([FromBody] Models.Dbo.Create.CreateUserProfileModel createUserProfileModel)
        {
            try
            {
                var result = await _userProfileService.CreateUserProfileAsync(createUserProfileModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create UserProfile.");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateUserProfile")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateUserProfileAsync([FromRoute] Guid id, [FromBody] Models.Dbo.Update.UpdateUserProfileModel updateUserProfileModel)
        {
            try
            {
                var userProfile = await _userProfileRepository.FindByIdAsync(id);

                if (userProfile == null)
                    return NotFound();

                await _userProfileService.UpdateUserProfileAsync(userProfile, updateUserProfileModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update UserProfile.");

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteUserProfile")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteUserProfileAsync([FromRoute] Guid id)
        {
            try
            {
                await _userProfileService.DeleteUserProfileAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete UserProfile.");

                return BadRequest(ex.Message);
            }
        }
    }
}