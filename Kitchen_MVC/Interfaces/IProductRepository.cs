using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Interfaces
{
    public interface IProductRepository
    {

        Task<ProductDTO> GetProductById(int id);

        Task<bool> CreateProduct(CreateProductRequest request);

        Task<bool> UpdateProduct(int id, UpdateProductRequest request);

        Task<bool> DeleteProduct(int id);

        List<ProductDTO> GetAllProducts();

        List<ImageDTO> GetImageById(int id);
    }
}
