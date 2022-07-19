using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace maker_checker_v1
{
    public class PolicyConfiguration
    {
        public static void setPolicies(AuthorizationOptions options)
        {

            options.AddPolicy("Client", policy => policy.RequireClaim(ClaimTypes.Role, "Client"));
            //TODO policy check if the role exists in database

        }
    }
}