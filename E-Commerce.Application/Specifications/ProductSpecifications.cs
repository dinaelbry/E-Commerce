using E_Commerce.Application.Params;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductSpecifications:BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductQueryParams productQueryParams) :base(
            p=>(!productQueryParams.brandId.HasValue) || p.BrandId== productQueryParams.brandId 
            && (!productQueryParams.typeId.HasValue) || p.TypeId == productQueryParams.typeId
            && (string.IsNullOrEmpty(productQueryParams.searchValue) || p.Name.ToLower().Contains(productQueryParams.searchValue.ToLower()))
            ) 
        { 
            AddInclude(p=> p.Brand); 
            AddInclude(p=> p.Type);

            switch (productQueryParams.sort)
            {
                case ProductSortingOptions.NameAsc: AddOrderBy(p=>p.Name);break;
                case ProductSortingOptions.NameDesc: AddOrderByDesc(p => p.Name); break;
                case ProductSortingOptions.PriceAsc: AddOrderBy(p => p.Price); break;
                case ProductSortingOptions.PriceDesc: AddOrderByDesc(p => p.Price); break;
                _: break;

            }

        }
        public ProductSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }

}
