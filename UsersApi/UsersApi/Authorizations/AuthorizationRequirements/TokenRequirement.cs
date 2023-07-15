using Microsoft.AspNetCore.Authorization;

namespace UsersApi.Authorizations.AuthorizationRequirements
{
    public class TokenRequirement : IAuthorizationRequirement
    {
        public int MinimumTokenAgeInMinutes { get; }

        public TokenRequirement(int minimumTokenAgeInMinutes)
        {
            MinimumTokenAgeInMinutes = minimumTokenAgeInMinutes;
        }
    }
}
