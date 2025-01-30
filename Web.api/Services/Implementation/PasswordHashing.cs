using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class PasswordHashing 
    {
        public static string HashPassword(Domain.Models.Account user, string password)
        {
            var options = new PasswordHasherOptions
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
            };

            var hasher = new PasswordHasher<Domain.Models.Account>(Options.Create(options));

            return hasher.HashPassword(user, password);
        }

        public static bool VerifyPassword(Domain.Models.Account user, string sourcePassword, string inputPassword)
        {
            var options = new PasswordHasherOptions
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
            };

            var hasher = new PasswordHasher<Domain.Models.Account>(Options.Create(options));

            var result = hasher.VerifyHashedPassword(user, sourcePassword, inputPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}
