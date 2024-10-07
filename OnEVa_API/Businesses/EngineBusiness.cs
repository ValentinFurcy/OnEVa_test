using AutoMapper;
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
	public class EngineBusiness : IEngineBusiness
	{
		private readonly IEngineRepository _engineRepository;
		private readonly IMapper _mapper;

		public EngineBusiness(IEngineRepository engineRepository, IMapper mapper)
		{
			_engineRepository = engineRepository;
			_mapper = mapper;
		}

		public async Task<EngineOutputDTO> CreateEngineAsync(EngineCreateInputDTO engineCreateInputDTO)
		{
			var isExist = await _engineRepository.GetEngineByLabelAsync(engineCreateInputDTO.Label);

			if (string.IsNullOrWhiteSpace(engineCreateInputDTO.Label))
			{
				throw new Exception("Engine format is not correct.");
			}
			else if (isExist != null)
			{
				throw new Exception($"This enigne : {engineCreateInputDTO.Label} already exists.");
			}
			else
			{
				Engine engine = _mapper.Map<Engine>(engineCreateInputDTO);
				return await _engineRepository.CreateEngineAsync(engine);
			}
		}

        public async Task<EngineOutputDTO> UpdateEngineAsync(EngineUpdateInputDTO engineUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(engineUpdateInputDTO.Label) || engineUpdateInputDTO.Id <= 0)
            {
                throw new Exception("Field required empty.");
            }
            else
            {
                var isExist = await _engineRepository.GetEngineByLabelAsync(engineUpdateInputDTO.Label);
                if (isExist != null && isExist.EngineId == engineUpdateInputDTO.Id)
                {
                    Engine engine = _mapper.Map<Engine>(engineUpdateInputDTO);
                    return await _engineRepository.UpdateEngineAsync(engine);
                }
                else if (isExist == null)
                {
                    Engine engine = _mapper.Map<Engine>(engineUpdateInputDTO);
                    return await _engineRepository.UpdateEngineAsync(engine);
                }
                else
                {
                    throw new Exception($"A Brand with this label ({engineUpdateInputDTO.Label}) already exists.");
                }
            }
        }

        public async Task<List<EngineOutputDTO>> GetAllEnginesAsync()
		{
			return await _engineRepository.GetAllEnginesAsync();
		}

		public async Task<EngineOutputDTO> GetEngineByIdAsync(int id)
		{
			return await _engineRepository.GetEngineByIdAsync(id);
		}

        public async Task<EngineOutputDTO> GetEngineByLabelAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new Exception("Engine format is not correct.");
            }
            else return await _engineRepository.GetEngineByLabelAsync(label);
        }

        public async Task<bool> DeleteEngineAsync(int id)
        {
            var engine = await _engineRepository.GetEngineByIdAsync(id);

            if (engine == null)
            {
                throw new Exception("Cannot delete a brand that does not exist");
            }
            else return await _engineRepository.DeleteEngineAsync(id);
        }
    }
}
