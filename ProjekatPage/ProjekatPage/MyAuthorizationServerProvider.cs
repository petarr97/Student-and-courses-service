using Microsoft.Owin.Security.OAuth;
using ProjekatPage.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace ProjekatPage
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);


            using (var baza = new PageBazaContext())
            {

                List<Korisnici> korisnici = baza.Korisnici.ToList();
                foreach (Korisnici k in korisnici)
                {
                    if (k.Username == context.UserName && k.Password == context.Password)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                        identity.AddClaim(new Claim("username", k.Username));
                        identity.AddClaim(new Claim(ClaimTypes.Name, k.Username));
                        context.Validated(identity);
                        return;
                    }
                }
            }
                context.SetError("invalid_grant", "Neispravni podaci za logovanje!!!");
           
        }
    }
}