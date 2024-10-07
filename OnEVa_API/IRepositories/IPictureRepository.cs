using DTOs.VehiclePropertiesDTOs.CreateInput;
using DTOs.VehiclePropertiesDTOs.Output;
using DTOs.VehiclePropertiesDTOs.UpdateInput;
using Entities;
using Entities.CONST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IPictureRepository
    {
        /// <summary>
        /// Add a new picture.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> CreatePictureAsync(Picture picture);

        /// <summary>
        /// Get all pictures.
        /// </summary>
        /// <returns>The list of pictures has the following format : PictureOutputDTO.</returns>
        public Task<List<PictureOutputDTO>> GetAllPicturesAsync();

        /// <summary>
        /// Get the picture corresponding to id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> GetPictureByIdAsync(int id);

		/// <summary>
		/// Get the list of pictures corresponding to the EngineId list
		/// </summary>
		/// <param name="pictureIds"></param>
		/// <returns>The list of pictures has the following format : Picture.</returns>
		public Task<List<Picture>> GetPicturesByIdsAsync(List<int> pictureIds);


		/// <summary>
		/// Get a picture corresponding to title.
		/// </summary>
		/// <param name="title"></param>
		/// <returns>A picture has the following format : PictureOutputDTO.</returns>
		public Task<List<PictureOutputDTO>> GetPicturesByTitleAsync(string title);

        /// <summary>
        /// Get a picture corresponding to title and color.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="idColor"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<List<PictureOutputDTO>> GetPicturesByTitleAndColorAsync(string title, Colors idColor);

        /// <summary>
        /// Get the picture corresponding to url.
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>A picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> GetPictureByUrlAsync(string Url);

        /// <summary>
        /// Update an existing picture.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns>The updated picture has the following format : PictureOutputDTO.</returns>
        public Task<PictureOutputDTO> UpdatePictureAsync(Picture picture);

        /// <summary>
        /// Delete brand if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if delete succed of False if unsucced.</returns>
        public Task<bool> DeletePictureAsync(int id);
    }
}
