using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")] // http://localhost:5001/api/products/sections
        public IEnumerable<SectionDTO> GetSections() => _ProductData.GetSections();

        [HttpGet("brands")] // http://localhost:5001/api/products/brands
        public IEnumerable<BrandDTO> GetBrands() => _ProductData.GetBrands();

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null) => 
            _ProductData.GetProducts(Filter ?? new ProductFilter());

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);
    }
}
