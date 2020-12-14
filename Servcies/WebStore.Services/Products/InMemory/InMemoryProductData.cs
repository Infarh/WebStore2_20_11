using System;
using System.Collections.Generic;
using System.Linq;

using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InMemory
{
    [Obsolete]
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<SectionDTO> GetSections() => TestData.Sections.AsEnumerable().Select(s => s.ToDTO());

        public SectionDTO GetSectionById(int id) => TestData.Sections.FirstOrDefault(s => s.Id == id).ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => TestData.Brands.AsEnumerable().Select(b => b.ToDTO());

        public BrandDTO GetBrandById(int id) => TestData.Brands.FirstOrDefault(b => b.Id == id).ToDTO();

        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            var total_count = query.Count();

            if (Filter?.PageSize > 0)
                query = query
                   .Skip((Filter.Page - 1) * (int) Filter.PageSize)
                   .Take((int) Filter.PageSize);


            return new PageProductsDTO
            {
                TotalCount = total_count,
                Products = query.ToDTO()
            };
        }

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();
    }
}
