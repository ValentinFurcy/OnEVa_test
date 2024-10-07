using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using Entities;
using IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public EngineRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EngineOutputDTO> CreateEngineAsync(Engine engine)
        {
            try
            {
                await _context.Engines.AddAsync(engine);
                await _context.SaveChangesAsync();

                return await GetEngineByIdAsync(engine.Id);
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error ! The new engine was not created", ex); 
            }
        }

        public async Task<EngineOutputDTO> UpdateEngineAsync(Engine engine)
        {
            try
            {
                var nbRows = await _context.Engines.Where(e => e.Id == engine.Id).ExecuteUpdateAsync (
                    updates => updates.SetProperty(e => e.Label, engine.Label));
                if (nbRows > 0)
                {
                    return await GetEngineByIdAsync(engine.Id);
                }
                throw new Exception("The update unsucced");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EngineOutputDTO>> GetAllEnginesAsync()
        {
            try
            {
                var engines = await _context.Engines.ToListAsync();
                

                if(engines.Any())
                {
                    var enginesOutputDTO = _mapper.Map<List<EngineOutputDTO>>(engines);
                    return enginesOutputDTO;
                }
                else return null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<EngineOutputDTO> GetEngineByIdAsync(int id)
        {
            try
            {
                var engine = await _context.Engines.FindAsync(id);

                if (engine != null)
                {
                    var engineOutputDTO = _mapper.Map<EngineOutputDTO>(engine);
                    return engineOutputDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EngineOutputDTO> GetEngineByLabelAsync(string label)
        {
            try
            {
                Engine engine = await _context.Engines.FirstOrDefaultAsync(e => e.Label == label);
                if (engine == null)
                {
                    return null;
                }
                else
                {
                    var engineOutputDTO = _mapper.Map<EngineOutputDTO>(engine);
                    return engineOutputDTO;
                }
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error retrieving data.", ex); 
            }
        }

        public async Task<bool> DeleteEngineAsync(int id)
        {
            try
            {
                var engineDeleted = await _context.Engines.Where(e => e.Id == id).ExecuteDeleteAsync();
                if (engineDeleted > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
