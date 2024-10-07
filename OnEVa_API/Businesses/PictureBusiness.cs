using AutoMapper;
using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using IBusinesses;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesses
{
    public class PictureBusiness : IPictureBusiness
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IMapper _mapper;

        public PictureBusiness(IPictureRepository pictureRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _mapper = mapper;
        }


        public async Task<PictureOutputDTO> CreatePictureAsync(PictureCreateInputDTO pictureCreateInputDTO)
        {
            var extistingUrl = await _pictureRepository.GetPictureByUrlAsync(pictureCreateInputDTO.Url);

            if (string.IsNullOrWhiteSpace(pictureCreateInputDTO.Title) || string.IsNullOrWhiteSpace(pictureCreateInputDTO.Url) || pictureCreateInputDTO.Color <= 0)
            {
                throw new Exception("Picture format is not correct.");
            }
            else if (extistingUrl != null)
            {
                throw new Exception($"This Picture ({pictureCreateInputDTO.Url}) already exists.");
            }
            else
            {
                if (Uri.TryCreate(pictureCreateInputDTO.Url, UriKind.Absolute, out Uri result) &&
                    (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                {
                    var picture = _mapper.Map<Picture>(pictureCreateInputDTO);
                    return await _pictureRepository.CreatePictureAsync(picture);
                }
                else throw new Exception("Picture Url format is not correct.");
            }
        }

        public async Task<List<PictureOutputDTO>> GetAllPicturesAsync()
        {
            return await _pictureRepository.GetAllPicturesAsync();
        }


        public async Task<PictureOutputDTO> GetPictureByIdAsync(int id)
        {
            return await _pictureRepository.GetPictureByIdAsync(id);
        }


        public async Task<List<PictureOutputDTO>> GetPicturesByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Picture format is not correct.");
            }
            else return await _pictureRepository.GetPicturesByTitleAsync(title);
        }

        public async Task<List<PictureOutputDTO>> GetPicturesByTitleAndColorAsync(string title, Colors idColor)
        {
            if (string.IsNullOrWhiteSpace(title) && idColor <= 0) 
            {
                throw new Exception("Picture format is not correct.");
            }
            else return await _pictureRepository.GetPicturesByTitleAndColorAsync(title, idColor);
        }

        public async Task<PictureOutputDTO> UpdatePictureAsync(PictureUpdateInputDTO pictureUpdateInputDTO)
        {
            if (string.IsNullOrWhiteSpace(pictureUpdateInputDTO.Title) || string.IsNullOrWhiteSpace(pictureUpdateInputDTO.Url) || pictureUpdateInputDTO.Color <= 0)
            {
                throw new Exception("Field required empty.");
            }
            else
            {
				var pictureUpdated = _mapper.Map<Picture>(pictureUpdateInputDTO);

				var extistingUrl = await _pictureRepository.GetPictureByUrlAsync(pictureUpdateInputDTO.Url);

                if (Uri.TryCreate(pictureUpdateInputDTO.Url, UriKind.Absolute, out Uri result) &&
                    (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                {
                    if (extistingUrl == null)
                    {
                        return await _pictureRepository.UpdatePictureAsync(pictureUpdated);
                    }
                    else if (extistingUrl != null && pictureUpdated.Id == extistingUrl.Id)
                    {
                        return await _pictureRepository.UpdatePictureAsync(pictureUpdated);
                    }
                    else
                    {
                        throw new Exception($"A Picture with this url ({pictureUpdateInputDTO.Url}) already exists.");
                    }
                }
                else throw new Exception("Picture Url format is not correct.");
            }
        }


        public async Task<bool> DeletePictureAsync(int id)
        {
            var picture = await _pictureRepository.GetPictureByIdAsync(id);

            if (picture == null)
            {
                throw new Exception("Cannot delete a brand that does not exist");
            }
            else return await _pictureRepository.DeletePictureAsync(id);
        }
    }
}
