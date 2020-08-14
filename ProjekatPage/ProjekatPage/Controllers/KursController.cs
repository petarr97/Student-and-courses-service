using ProjekatPage.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjekatPage.Controllers
{
    public class KursController : ApiController
    {
        [Authorize(Roles = "admin")]
        [Route("getKursevi")]
        [HttpGet]
        public IEnumerable<Kurs> getKursevi()
        {
            using (var context = new PageBazaContext())
            {
                return context.Kurs.ToList();
            }
        }

        [Authorize(Roles = "admin")]
        [Route("getKurs")]
        [HttpGet]
        public Kurs getKurs(int id)
        {
            using (var context = new PageBazaContext())
            {
                var pomocna = context.Kurs.Where(k => k.Kursid == id).ToList();
                Kurs kurs = new Kurs();
                kurs.Nazivkursa = pomocna[0].Nazivkursa;

                return kurs;
            }
        }

        [Authorize(Roles = "admin")]
        [Route("getStudentKursList")]
        [HttpGet]
        public IEnumerable<Studentkurs> getStudentKurs(int id)
        {
            using (var context = new PageBazaContext())
            {

                return context.Studentkurs.ToList();
            }
        }

        [Authorize(Roles = "admin")]
        [Route("kreirajKurs")]
        [HttpGet]
        public void kreirajKurs(string Ime, [FromUri] int[] niz)
        {
            using (var context = new PageBazaContext())
            {
                Kurs kurs = new Kurs();
                kurs.Nazivkursa = Ime;
                context.Add(kurs);
                context.SaveChanges();
                var a = context.Kurs.Where(cur => cur.Nazivkursa == Ime).ToList();
                int poslednjid = a[0].Kursid;
                for (int i = 0; i < niz.Length; i++)
                {
                    Studentkurs stud = new Studentkurs();
                    stud.Kursid = poslednjid;
                    stud.Studentid = niz[i];

                    context.Add(stud);
                    context.SaveChanges();
                }
            }
        }
    }
}
