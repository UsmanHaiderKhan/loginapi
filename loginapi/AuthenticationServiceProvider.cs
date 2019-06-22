using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace loginapi
{
    public class AuthenticationServiceProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (context.UserName == "Admin" && context.Password == "Admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("UserName", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Usman haider khan"));
                context.Validated(identity);

            }
            else if (context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("UserName", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Arslan Ameer"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Permission DENIED", "Invalid UserName Or Password... !Try Again");
                return;
            }
        }
    }
}
