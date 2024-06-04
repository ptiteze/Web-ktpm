using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;
using System.Collections;

namespace Kitchen_MVC.Repositores
{
    public class ProductRepository: IProductRepository
    {
        //private readonly DataContext _dataContext;
        //private readonly IMapper _mapper;


        public ProductRepository(/*DataContext dataContext, IMapper mapper*/)
        {
            //_dataContext = dataContext;
            //_mapper = mapper;
        }

        public Task<bool> CreateProduct(CreateProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetAllProducts()
        {

            var products = SingletonDataBridge.GetInstance().Products.Where(p => p.Status == true).ToList();
            var productDtos = new List<ProductDTO>();
            products.ForEach(x => productDtos.Add(SingletonAutoMapper.GetInstance().Map<ProductDTO>(x)));
            return productDtos;
        }

		public List<ImageDTO> GetImageById(int id)
		{
            var images = SingletonDataBridge.GetInstance().Images.Where(i => i.ProductId == id).ToList();
            var imageDtos = new List<ImageDTO>();
            images.ForEach(x => imageDtos.Add(SingletonAutoMapper.GetInstance().Map<ImageDTO>(x)));
            return imageDtos;
		}

		public ProductDTO GetProductById(int id)
        {
            var product = SingletonDataBridge.GetInstance().Products.Where(p => p.Id == id).FirstOrDefault();

            return SingletonAutoMapper.GetInstance().Map<ProductDTO>(product);
        }

        public Task<bool> UpdateProduct(int id, UpdateProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
