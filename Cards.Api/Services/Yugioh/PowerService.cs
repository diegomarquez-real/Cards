using AutoMapper;

namespace Cards.Api.Services.Yugioh
{
    public class PowerService : Abstractions.IPowerService
    {
        private readonly IMapper _mapper;
        private readonly Data.Abstractions.Repositories.Yugioh.IPowerRepository _powerRepository;

        public PowerService(IMapper mapper,
            Data.Abstractions.Repositories.Yugioh.IPowerRepository powerRepository)
        {
            _mapper = mapper;
            _powerRepository = powerRepository;
        }

        public async Task<Models.Yugioh.PowerModel> GetPowerAsync(Guid powerId)
        {
            var power = await _powerRepository.FindByIdAsync(powerId);

            return _mapper.Map<Models.Yugioh.PowerModel>(power);
        }

        public async Task<Guid> CreatePowerAsync(Models.Yugioh.Create.CreatePowerModel createPowerModel)
        {
            var power = _mapper.Map<Data.Models.Power>(createPowerModel);
            var result = await  _powerRepository.CreateAsync(power);

            return result.PowerId;
        }

        public async Task UpdatePowerAsync(Models.Yugioh.PowerModel powerModel, Models.Yugioh.Update.UpdatePowerModel updatePowerModel)
        {
            var power = _mapper.Map<Data.Models.Power>(updatePowerModel);
            power.PowerId = powerModel.PowerId;
            power.CardId = powerModel.CardId;

            await _powerRepository.UpdateAsync(power);
        }

        public async Task DeletePowerAsync(Guid powerId)
        {
            await _powerRepository.DeleteAsync(powerId);
        } 
    }
}