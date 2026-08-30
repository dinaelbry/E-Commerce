using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Product;
using E_Commerce.Application.Params;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(ProductQueryParams productQueryParams, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(productQueryParams);
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(spec,ct);

            var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(Products);
            return Result<IReadOnlyList<ProductDto>>.Ok(mappedProducts);

        }

        public  async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await unitOfWork.GetRepository<ProductsBrand, int>().GetAllAsync(ct);

            var mappedBrands = mapper.Map<IReadOnlyList<ProductsBrand>, IReadOnlyList<BrandDto>>(Brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(mappedBrands);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var Types = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync (ct);

            var mappedTypes = mapper.Map<IReadOnlyList<ProductsType>, IReadOnlyList<TypeDto>>(Types);
            return Result<IReadOnlyList<TypeDto>>.Ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(id);
            var product = await unitOfWork.GetRepository<Product,int>().GetByIdWithSpecificationsAsync(spec,ct);

            if (product is null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product with id: {id} is not found"));

            var mappedProduct = mapper.Map<Product,ProductDto>(product);

            return mappedProduct;
            
        }
    }
}
