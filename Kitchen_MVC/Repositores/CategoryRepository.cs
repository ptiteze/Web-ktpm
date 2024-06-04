using AutoMapper;
using Kitchen_MVC.Commons.Exceptions;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;
using Microsoft.IdentityModel.Tokens;

namespace Kitchen_MVC.Repositores
{
	public class CategoryRepository : ICategoryRepository
    {
        //private readonly DataContext SingletonDataBridge.GetInstance();
        //private readonly IMapper SingletonAutoMapper.GetInstance();

        public CategoryRepository(/*DataContext dataContext, IMapper mapper*/)
        {
            //SingletonDataBridge.GetInstance() = dataContext;
            //SingletonAutoMapper.GetInstance() = mapper;
        }

        public async Task<bool> CreateCategory(CreateCategoryRequest request)
        {
            bool isSuccess = false;
            try
            {
                var category = SingletonAutoMapper.GetInstance().Map<Category>(request);

                SingletonDataBridge.GetInstance().Categories.Add(category);
                await SingletonDataBridge.GetInstance().SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception) { 
            }
            
            return isSuccess;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            bool isSuccess = false;
            try
            {
                var category = SingletonDataBridge.GetInstance().Categories.Find(id);

                if (category == null)
                {
                    throw new NotFoundException("Category don't exists");
                }

                var products = SingletonDataBridge.GetInstance().Products.Where(x => x.CategoryId == id);
                if (!products.IsNullOrEmpty())
                {
                    throw new InvalidRequestException("No delete category because it exist product");
                }
                SingletonDataBridge.GetInstance().Categories.Remove(category);
                await SingletonDataBridge.GetInstance().SaveChangesAsync();
                isSuccess=true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var categories = SingletonDataBridge.GetInstance().Categories.ToList();

			return SingletonAutoMapper.GetInstance().Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var cartegory = SingletonDataBridge.GetInstance().Categories.Find(id);

			return SingletonAutoMapper.GetInstance().Map<CategoryDTO>(cartegory);
        }

		public List<ProductDTO> GetProductsByCategoryId(int id)
		{
            var products = SingletonDataBridge.GetInstance().Products.Where(p => p.CategoryId == id && p.Status == true).ToList();
			return SingletonAutoMapper.GetInstance().Map<List<ProductDTO>>(products);
		}

		public async Task<bool> UpdateCategory(int id,UpdateCategoryRequest request)
        {
            bool isSuccess = false;
            try
            {
                var category = SingletonDataBridge.GetInstance().Categories.Find(id);

                if (category == null)
                {
                    throw new NotFoundException();
                }
                category.Name = request.Name;
                SingletonDataBridge.GetInstance().Categories.Update(category);
                await SingletonDataBridge.GetInstance().SaveChangesAsync();
                isSuccess = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }
		
	}
}
