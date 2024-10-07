using AutoMapper;
using DTOs.StateDTOs;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public StateRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StateOutputDTO> CreateStateAsync(State state)
        {
            try
            {
                await _context.States.AddAsync(state);
                await _context.SaveChangesAsync();

                return await GetStateByIdAsync(state.Id);
            }
            catch (Exception ex) { throw new Exception("Error ! The new state was not created", ex); }
        }

        public async Task<List<StateOutputDTO>> GetAllStatesAsync()
        {
            try
            {
                var states = await _context.States.ToListAsync();
                if (states.Any())
                {
                    var stateOutputDTO = _mapper.Map<List<StateOutputDTO>>(states);
                    return stateOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<StateOutputDTO> GetStateByIdAsync(int id)
        {
            try
            {
                var state = await _context.States.FindAsync(id);
                if (state != null)
                {
                    var stateOutputDTO = _mapper.Map<StateOutputDTO>(state);
                    return stateOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<StateOutputDTO> GetStateByLabelAsync(string label)
        {
            try
            {
                State state = await _context.States.FirstOrDefaultAsync(e => e.Label == label);
                if (state == null)
                {
                    return null;
                }
                else
                {
                    return new StateOutputDTO { Label = state.Label };
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<StateOutputDTO> UpdateStateAsync(State state)
        {
            try
            {
                await _context.States.Where(e => e.Id == state.Id).ExecuteUpdateAsync(
                    updates => updates.SetProperty(e => e.Label, state.Label));
                var stateOutputDTO = _mapper.Map<StateOutputDTO>(state);
                return stateOutputDTO;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<bool> DeleteStateAsync(int id)
        {
            try
            {
                var stateId = await _context.States.FindAsync(id);
                if (stateId != null)
                {
                    await _context.States.Where(e => e.Id == id).ExecuteDeleteAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}

