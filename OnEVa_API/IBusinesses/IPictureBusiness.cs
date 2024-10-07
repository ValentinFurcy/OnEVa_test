using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IBusinesses
{
    public interface IPictureBusiness
    {

        /// <summary>
        /// Add a new picture.
        /// </summary>
        /// <param name="pictureCreateInputDTO"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> CreatePictureAsync(PictureCreateInputDTO pictureCreateInputDTO);

        /// <summary>
        /// Get all picture.
        /// </summary>
        /// <returns>The list of pictures has the following format : PictureOutputDTO./returns>
        public Task<List<PictureOutputDTO>> GetAllPicturesAsync();

        /// <summary>
        /// Get a picture corresponding to id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> GetPictureByIdAsync(int id);

        /// <summary>
        /// Get a picture corresponding to title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<List<PictureOutputDTO>> GetPicturesByTitleAsync(string title);

        /// <summary>
        ///  Get a picture corresponding to title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="idColor"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<List<PictureOutputDTO>> GetPicturesByTitleAndColorAsync(string title, Colors idColor);
  
        /// <summary>
        /// Update an existing picture.
        /// </summary>
        /// <param name="pictureUpdateInputDTO"></param>
        /// <returns>The updated picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> UpdatePictureAsync(PictureUpdateInputDTO pictureUpdateInputDTO);

        /// <summary>
        /// Delete brand if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeletePictureAsync(int id);
    }
}
