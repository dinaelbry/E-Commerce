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
                p => (!productQueryParams.BrandId.HasValue || p.BrandId == productQueryParams.BrandId)
                  && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
                  && (string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower()))
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
                default: AddOrderBy(p => p.Id); break;
            }

            ApplyPagination(productQueryParams.PageSize, productQueryParams.PageIndex);

        }
        public ProductSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }

}
