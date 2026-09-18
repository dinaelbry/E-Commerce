using E_Commerce.Application.Common;
using E_Commerce.Application.DTO_s.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketServices
    {
        Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default);
    }
}
