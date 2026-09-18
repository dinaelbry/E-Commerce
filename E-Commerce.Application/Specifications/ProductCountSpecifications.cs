using E_Commerce.Application.Params;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams productQueryParams) : base(
                p => (!productQueryParams.BrandId.HasValue || p.BrandId == productQueryParams.BrandId)
                  && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
                  && (string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower()))
                        )
        {
        }
    }
}
