using Entities.CONST;

namespace DTOs.VehiclePropertiesDTOs.CreateInput;
public class PictureCreateInputDTO
{
    public string Title { get; set; }
    public string Url { get; set; }
    public Colors Color { get; set; }
}
