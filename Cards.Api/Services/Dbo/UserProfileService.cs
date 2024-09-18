using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Cards.Api.Services.Dbo
{
    public class UserProfileService : Abstractions.IUserProfileService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Dbo.IUserProfileRepository _userProfileRepository;
        private readonly IValidator<Data.Models.Dbo.UserProfile> _validator;
        private readonly IValidator<Validators.Dbo.Models.UserProfilePasswordModel> _userProfilePasswordValidator;
        private readonly IPasswordHasher<Data.Models.Dbo.UserProfile> _passwordHasher;

        public UserProfileService(IMapper mapper,
            Data.Abstractions.Repositories.Dbo.IUserProfileRepository userProfileRepository,
            IValidator<Data.Models.Dbo.UserProfile> validator,
            IValidator<Validators.Dbo.Models.UserProfilePasswordModel> userProfilePasswordValidator,
            IPasswordHasher<Data.Models.Dbo.UserProfile> passwordHasher)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _validator = validator;
            _userProfilePasswordValidator = userProfilePasswordValidator;
            _passwordHasher = passwordHasher;
        }

        public async Task<Models.Dbo.UserProfileModel> GetUserProfileAsync(Guid userProfileId)
        {
            var userProfile = await _userProfileRepository.FindByIdAsync(userProfileId);

            return _mapper.Map<Models.Dbo.UserProfileModel>(userProfile);
        }

        public async Task<Guid> CreateUserProfileAsync(Models.Dbo.Create.CreateUserProfileModel createUserProfileModel)
        {

            var userProfile = _mapper.Map<Data.Models.Dbo.UserProfile>(createUserProfileModel);
            var userProfileValidationModel = new Validators.Dbo.Models.UserProfilePasswordModel() 
            {
                UserProfile = userProfile,
                Password = new Validators.Dbo.Models.PasswordModel() { Password = createUserProfileModel.Password }
            };
            await _userProfilePasswordValidator.ValidateAndThrowAsync(userProfileValidationModel);
            userProfile.PasswordHash = _passwordHasher.HashPassword(userProfile, createUserProfileModel.Password);
            var result = await _userProfileRepository.CreateAsync(userProfile);

            return result.UserProfileId;

        }

        public async Task UpdateUserProfileAsync(Data.Models.Dbo.UserProfile userProfileModel, Models.Dbo.Update.UpdateUserProfileModel updateUserProfileModel)
        {
            var userProfile = _mapper.Map(updateUserProfileModel, userProfileModel);
            await _validator.ValidateAndThrowAsync(userProfile);
            await _userProfileRepository.UpdateAsync(userProfile);
        }

        public async Task DeleteUserProfileAsync(Guid userProfileId)
        {
            // We would not want to delete a user profile, but rather deactivate it.
            await _userProfileRepository.DeactivateUserProfileAsync(userProfileId);
        }
    }
}