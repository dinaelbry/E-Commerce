using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTO_s.Authentications
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
