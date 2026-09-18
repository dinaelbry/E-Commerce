using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Authentications;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class IdentityServices : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result<bool>.Fail(new Error("UserNotFound", $"User with email '{email}' not found."));
            }

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return Result<bool>.Ok(isValid);
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await _userManager.FindByEmailAsync(email) is not null
                ? Result<bool>.Ok(true)
                : Result<bool>.Ok(false);
        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result<IdentityUserResult>.Fail(new Error("UserNotFound", $"User with email '{email}' not found."));

            }
            else
            {
             
                return Result<IdentityUserResult>.Ok(new IdentityUserResult (user.Id, user.DisplayName,user.Email, user.UserName));
            }
        }

        public async Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken ct = default)
        {
           var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            if (user is null) return Result<AddressDto>.Fail(Error.NotFound("UserNotFound"));
            
            if (user.Address is null) return Result<AddressDto>.Fail(Error.NotFound("AddressNotFound"));
            
           
            return Result<AddressDto>.Ok(new AddressDto()
            {
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName,
                Street = user.Address.Street,
                City = user.Address.City,
                Country = user.Address.Country
            });
        }

        public async Task<Result<IEnumerable<string>>> GetRolesAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result<IEnumerable<string>>.Fail(Error.NotFound("User.NotFound", $"User with email '{email}' not found."));
            }
            var roles = await _userManager.GetRolesAsync(user);
            return Result<IEnumerable<string>>.Ok(roles);
        }

        public async Task<Result<AddressDto>> UpdateAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            
            if (user is null) return Result<AddressDto>.Fail(Error.NotFound("UserNotFound"));

            if (user.Address is null) {
                user.Address = new Address()
                {
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    Street = addressDto.Street,
                    City = addressDto.City,
                    Country = addressDto.Country
                };
            }
            else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
 
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return Result<AddressDto>.Fail(Error.Failure("AddressUpdateFailed", "Failed to update address."));

            return Result<AddressDto>.Ok(addressDto);
        }
    }
}
 