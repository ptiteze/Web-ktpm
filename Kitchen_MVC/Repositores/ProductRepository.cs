using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using System.Collections;

namespace Kitchen_MVC.Repositores
{
    public class ProductRepository: IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var products = _context.Products.Where(p => p.Status == true).ToList();
            var productDtos = new List<ProductDTO>();
            products.ForEach(x => productDtos.Add(_mapper.Map<ProductDTO>(x)));
            return productDtos;
        }

		public List<ImageDTO> GetImageById(int id)
		{
            var images = _context.Images.Where(i => i.ProductId == id).ToList();
            var imageDtos = new List<ImageDTO>();
            images.ForEach(x => imageDtos.Add(_mapper.Map<ImageDTO>(x)));
            return imageDtos;
		}

		public ProductDTO GetProductById(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();

            return _mapper.Map<ProductDTO>(product);
        }

        public Task<bool> UpdateProduct(int id, UpdateProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
