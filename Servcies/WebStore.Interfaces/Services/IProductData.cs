using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();

        IEnumerable<BrandDTO> GetBrands();

        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);

        ProductDTO GetProductById(int id);
    }
}
