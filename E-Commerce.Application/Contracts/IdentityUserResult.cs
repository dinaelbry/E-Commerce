using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public sealed class IdentityUserResult
    {
        public IdentityUserResult (string id, string displayName, string? email = null, string? userName = null)
        {
            Id = id;
            Email = email;
            UserName = userName;
            DisplayName = displayName;
        }
         public string Id { get; set; } = default!;
       public string? Email { get; set; }
        public string? UserName { get; set; }
        public string DisplayName { get; set; } = default!;
    }
}
