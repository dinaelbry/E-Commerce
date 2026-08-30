using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common
{
    public record Error(string Code, string Description, ErrorType Type = ErrorType.Failure)
    {
        public static Error Failure(string code= "General.Failure", string Description = "General Failure Description")
            => new Error (code, Description, ErrorType.Failure);

        public static Error Validation(string code = "General.Validation", string Description = "General Validation Description")
             => new Error(code, Description, ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string Description = "General NotFound Description")
            => new Error(code, Description, ErrorType.NotFound);
        public static Error Conflict(string code = "General.Conflict", string Description = "General Conflict Description")
             => new Error(code, Description, ErrorType.Conflict);
        public static Error UnAuthorized(string code = "General.UnAuthorized", string Description = "General UnAuthorized Description")
            => new Error(code, Description, ErrorType.UnAuthorized);
        public static Error Forbidden(string code = "General.Failure", string Description = "General Forbidden Description")
            => new Error(code, Description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string Description = "General InvalidCredentials Description")
            => new Error(code, Description, ErrorType.InvalidCredentials);

    }
}
