using Kitchen_MVC.Commons.Responses;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Services;
using Kitchen_MVC.Singleton;
using Microsoft.EntityFrameworkCore;

namespace Kitchen_MVC.Repositores
{
    public class ImageRepository : IImageRepository
    {
        private readonly IUploadService _upload;

        public ImageRepository(IUploadService uploadService)
        {
            _upload = uploadService;
        }

        public async Task<bool> CreateImage(CreateImageRequest request)
        {
            var product = await SingletonDataBridge.GetInstance().Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return false;
            }
            var image = new Image()
            {
                ProductId = request.ProductId,
                Product = product,
            };

            if (request.Url != null)
            {
                image.Url = await _upload.UploadFile(request.Url);
            }

            SingletonDataBridge.GetInstance().Images.Add(image);
            SingletonDataBridge.GetInstance().SaveChanges();
            return true; 
        }

        public Task<bool> DeleteImage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageDTO>> GetAllImages(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageDTO> GetImageById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
