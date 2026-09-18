using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Authentications;

namespace E_Commerce.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }


        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            // Get User By Email 
            var userResult = await _identityService.FindByEmailAsync(loginDto.Email, ct);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            // Check Password 
            var passwordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(Error.UnAuthorized("Invalid Email or Password"));

            var user = userResult.data!;
            var rolesResult = await _identityService.GetRolesAsync(user.Email!, ct);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);
            var roles = rolesResult.data;
            var token = _tokenService.CreateToken(user.Id, user.Email!, user.UserName!, rolesResult.data!);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var createResult = await _identityService.CreateUserAsync(registerDto, ct);
            if (!createResult.IsSuccess)
                return Result<UserDto>.Fail(createResult.Errors);

            var newUser = createResult.data!;
            var rolesResult = await _identityService.GetRolesAsync(newUser.Email!, ct);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);

            var token = _tokenService.CreateToken(newUser.Id, newUser.Email!, newUser.UserName!, rolesResult.data!);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = newUser.Email!,
                DisplayName = newUser.DisplayName,
                Token = token
            });

        }




        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default)
                => await _identityService.EmailExistsAsync(email, ct);


        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindByEmailAsync(email, ct);

            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var user = userResult.data!;
            var rolesResult = await _identityService.GetRolesAsync(user.Email!);

            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);

            var token = _tokenService.CreateToken(user.Id, user.Email!, user.UserName!, rolesResult.data!);
            
            return Result<UserDto>.Ok(new UserDto() { 
                Email = user.Email!, 
                DisplayName = user.DisplayName, 
                Token = token 
            });
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var result = await _identityService.GetAddressByEmailAsync(email, ct);
            if (!result.IsSuccess)
                return Result<AddressDto>.Fail(result.Errors);
            return Result<AddressDto>.Ok(result.data!);
        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default)
                => await _identityService.UpdateAddressAsync(email, addressDto, ct);

    }
}