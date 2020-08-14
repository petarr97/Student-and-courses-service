using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using ProjekatPage.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjekatPage.Controllers
{
    public class DataController : ApiController
    {
        
        
        [Authorize]
        [HttpPost]
        [Route("api/data/authenticate")]
        public IHttpActionResult getForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [HttpPost]
        [Route("api/data/getForAdmin")]
        public IHttpActionResult getForAdmin([FromBody]LoginRequestModel user)
        {
            using (var baza = new PageBazaContext())
            {
                List<Korisnici> korisnici = baza.Korisnici.ToList();
                foreach (Korisnici k in korisnici)
                {
                 if(user.Username ==k.Username && user.Password == k.Password)
                    {
                        var identity = (ClaimsIdentity)User.Identity;
                        var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                        foreach (var rol in roles)
                            if (rol == "admin")
                                return Ok(true);
                        return Ok(false);
                    }
                }
                return Ok(false);

            }
        }

        [HttpPost]
        [Route("api/data/registrujKorisnika")]
        public IHttpActionResult registrujKorisnika([FromBody]LoginRequestModel user)
        {
            Boolean poklapanje = false;
            using (var baza = new PageBazaContext())
            {
                List<Korisnici> korisnici = baza.Korisnici.ToList();
                foreach (Korisnici k in korisnici)
                {
                    if (user.Username == k.Username )
                    {
                        poklapanje = true;
                        return Ok("Username "+user.Username+" vec postoji!");
                    }
                }
                Korisnici kor = new Korisnici();
                kor.Username = user.Username;
                kor.Password = user.Password;

                baza.Add(kor);
                baza.SaveChanges();
                return Ok(true);

            }
        }

    }
}
