using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(ProjekatPage.Startup))]

namespace ProjekatPage
{

    public class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthorizationServerProvider();
              OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions {
                  AllowInsecureHttp = true,
                  TokenEndpointPath = new PathString("/token"),
                  AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                  Provider = myProvider,
          };
              app.UseOAuthAuthorizationServer(options);
              app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
           

            HttpConfiguration config = new HttpConfiguration();
              WebApiConfig.Register(config);
          }
        }
    
}
