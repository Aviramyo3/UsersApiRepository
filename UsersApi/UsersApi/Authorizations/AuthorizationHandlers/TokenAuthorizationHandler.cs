using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UsersApi.Authorizations.AuthorizationRequirements;
using Microsoft.AspNetCore.Http;

namespace UsersApi.Authorizations.AuthorizationHandlers
{
    public class TokenAuthorizationHandler : AuthorizationHandler<TokenRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,TokenRequirement requirement)
        {
            //TODO -  move to settings
            //Move to TokenRequirement
            string userId = "nuUOk3UjNkTeCf1LiCgPZW8Faml1";

            IEnumerable<Claim> userClaims = context.User.Claims;
            if (userClaims.Any())
            {
                string UserId = userClaims.FirstOrDefault(c => c.Type == "id")?.Value;

                if (UserId == userId)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            
            return Task.CompletedTask;
        }
    }
}
