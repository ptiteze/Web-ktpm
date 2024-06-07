using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Admin
{
    public class AddProductViewModel
    {
        public List<CategoryDTO> Categories { get; set; }

        public CreateProductRequest request { get; set; }
    }
}
