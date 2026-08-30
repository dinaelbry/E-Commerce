
using E_Commerce.Application.Common;
using E_Commerce.Application.DTO_s.Product;
using E_Commerce.Application.Params;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IProductServices
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(ProductQueryParams productQueryParams, CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default);

        Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
