using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct)
            => ToActionResult(await _authenticationService.LoginAsync(loginDto, ct));

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct)
            => ToActionResult(await _authenticationService.RegisterAsync(registerDto, ct));

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email, CancellationToken ct)
        => ToActionResult(await _authenticationService.CheckEmailAsync(email, ct));

        [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct)
            => ToActionResult(await _authenticationService.GetCurrentUserAsync(GetEmailFromToken(), ct));

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress(CancellationToken ct)
            => ToActionResult(await _authenticationService.GetUserAddressAsync(GetEmailFromToken(), ct));

        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromQuery] string email,[FromBody] AddressDto addressDto, CancellationToken ct)
            => ToActionResult(await _authenticationService.UpdateUserAddressAsync(addressDto, email, ct));

    }
}
