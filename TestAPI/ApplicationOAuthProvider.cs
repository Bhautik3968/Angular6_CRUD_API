using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.DataContext;
namespace TestAPI
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {           
            User user = new User();
            user.Username = context.UserName;
            user.Password = context.Password;
            UserDataContext _objContext = new UserDataContext();
            user = _objContext.GetUser(user);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Username", user.Username));
                identity.AddClaim(new Claim("Password", user.Password));
                identity.AddClaim(new Claim("ID", user.ID));
                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Unauthorized", "The user name or password is incorrect");
                return;
            }
                
        }
    }
}