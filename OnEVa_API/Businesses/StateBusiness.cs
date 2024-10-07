using AutoMapper;
using DTOs.StateDTOs;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using IBusinesses;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class StateBusiness : IStateBusiness
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateBusiness(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<StateOutputDTO> CreateStateAsync(StateCreateInputDTO stateInputDTO)
        {
            var isExist = await _stateRepository.GetStateByLabelAsync(stateInputDTO.Label);

            if (string.IsNullOrWhiteSpace(stateInputDTO.Label))
            {
                throw new Exception("State format is not correct.");
            }
            else if (isExist != null)
            {
                throw new Exception($"This state : {stateInputDTO.Label} already exists.");
            }
            else
            {
                State state = new State { Label = stateInputDTO.Label };
                return await _stateRepository.CreateStateAsync(state);
            }
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            return await _stateRepository.DeleteStateAsync(id);
        }

        public async Task<List<StateOutputDTO>> GetAllStatesAsync()
        {
            return await _stateRepository.GetAllStatesAsync();
        }

        public async Task<StateOutputDTO> GetStateByIdAsync(int id)
        {
            return await _stateRepository.GetStateByIdAsync(id);
        }

        public async Task<StateOutputDTO> UpdateStateAsync(StateUpdateInputDTO stateInputDTO)
        {
            var isExist = await _stateRepository.GetStateByLabelAsync(stateInputDTO.Label);

            if (string.IsNullOrWhiteSpace(stateInputDTO.Label))
            {
                throw new Exception("State format is not correct.");
            }
            else if (isExist != null)
            {
                throw new Exception($"This state : {stateInputDTO.Label} already exists.");
            }
            else
            {
                var state = new State 
                { 
                    Id = stateInputDTO.Id,
                    Label = stateInputDTO.Label 
                };
                return await _stateRepository.UpdateStateAsync(state);
            }
        }
    }
}
