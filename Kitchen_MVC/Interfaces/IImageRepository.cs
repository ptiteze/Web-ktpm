using Kitchen_MVC.Commons.Responses;
using Kitchen_MVC.DTO.Image;

namespace Kitchen_MVC.Interfaces
{
    public interface IImageRepository
    {
        Task<ImageDTO> GetImageById(int id);

        Task<bool> CreateImage(CreateImageRequest request);

        Task<bool> DeleteImage(int id);

        Task<List<ImageDTO>> GetAllImages(int productId);
    }
}
