using E_Commerce.Application.Common;
using E_Commerce.Application.DTO_s.Authentications;
using E_Commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto,CancellationToken ct = default);
        Task<Result<IEnumerable<string>>> GetRolesAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> UpdateAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default); // create & update
        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default);
    }
}
