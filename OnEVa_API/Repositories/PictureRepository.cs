using AutoMapper;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using IRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public PictureRepository(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<PictureOutputDTO> CreatePictureAsync(Picture picture)
        {
            try
            {
                await _context.Pictures.AddAsync(picture);
                await _context.SaveChangesAsync();

                return await GetPictureByIdAsync(picture.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error ! The new picture was not created");
            };
        }


        public async Task<List<PictureOutputDTO>> GetAllPicturesAsync()
        {
            try
            {
                var picturesList = await _context.Pictures.ToListAsync();
                var pictures = _mapper.Map<List<PictureOutputDTO>>(picturesList);

                if (pictures.Any())
                {
                    return pictures;
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

        public async Task<PictureOutputDTO> GetPictureByIdAsync(int id)
        {
            try
            {
                var picture = await _context.Pictures.FindAsync(id);
                if (picture != null)
                {
                    var pictureOutputDTO = _mapper.Map<PictureOutputDTO>(picture);
                    return pictureOutputDTO;
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
        public async Task<List<Picture>> GetPicturesByIdsAsync(List<int> pictureIds)
        {
            try
            {

                var pictures = await _context.Pictures.Where(p => pictureIds.Contains(p.Id)).ToListAsync();

                if (pictures.Any())
                {
                    return pictures;
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

        public async Task<List<PictureOutputDTO>> GetPicturesByTitleAsync(string title)
        {
            try
            {
                var picture = await _context.Pictures.Where(p => p.Title == title).ToListAsync();

                if (!picture.Any())
                {
                    return null;
                }
                else
                {
                    var pictureOutputDTO = _mapper.Map<List<PictureOutputDTO>>(picture);
                    return pictureOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data", ex);
            }
        }

        public async Task<List<PictureOutputDTO>> GetPicturesByTitleAndColorAsync(string title, Colors idColor)
        {
            try
            {
                List<Picture> picture = await _context.Pictures.Where(p => p.Title == title && p.Color == idColor).ToListAsync();

                if (picture == null)
                {
                    return null;
                }
                else
                {
                    var pictureOutputDTO = _mapper.Map<List<PictureOutputDTO>>(picture);
                    return pictureOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data", ex);
            }
        }

        public async Task<PictureOutputDTO> GetPictureByUrlAsync(string Url)
        {
            try
            {
                Picture picture = await _context.Pictures.FirstOrDefaultAsync(p => p.Url == Url);
                if (picture == null)
                {
                    return null;
                }
                else
                {
                    var pictureOutputDTO = _mapper.Map<PictureOutputDTO>(picture);
                    return pictureOutputDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data", ex);
            }
        }

        public async Task<PictureOutputDTO> UpdatePictureAsync(Picture picture)
        {
            try
            {
                var nbRows = await _context.Pictures.Where(p => p.Id == picture.Id).ExecuteUpdateAsync(
                    updates => updates
                    .SetProperty(p => p.Title, picture.Title)
                    .SetProperty(p => p.Url, picture.Url)
                    .SetProperty(p => p.Color, picture.Color));
                    
                if (nbRows > 0)
                {
                    var pictureOutputDTO = _mapper.Map<PictureOutputDTO>(picture);
                    return pictureOutputDTO;
                }
                throw new Exception("The update failed");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeletePictureAsync(int id)
        {
            try
            {
                var pictureDeleted = await _context.Pictures.Where(b => b.Id == id).ExecuteDeleteAsync();
                if (pictureDeleted > 0)
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
