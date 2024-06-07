using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Image
{
    public class AddImageViewModel
    {
        public List<ProductDTO> Products { get; set; }

        public CreateImageRequest request { get; set; }
    }
}
