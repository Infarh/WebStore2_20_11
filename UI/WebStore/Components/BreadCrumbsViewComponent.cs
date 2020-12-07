using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke()
        {
            var model = new BreadCrumbsViewModel();

            if (int.TryParse(Request.Query["SectionId"], out var section_id))
            {
                model.Section = _ProductData.GetSectionById(section_id).FromDTO();
                if (model.Section.ParentId != null)
                    model.Section.ParentSection = _ProductData.GetSectionById((int) model.Section.ParentId).FromDTO();
            }

            if (int.TryParse(Request.Query["BrandId"], out var brand_id)) 
                model.Brand = _ProductData.GetBrandById(brand_id).FromDTO();

            if (int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out var product_id))
            {
                var product = _ProductData.GetProductById(product_id);
                if (product is not null)
                    model.Product = product.Name;
            }

            return View(model);
        }
    }
}
