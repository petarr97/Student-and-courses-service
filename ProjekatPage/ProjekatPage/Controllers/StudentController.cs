using Microsoft.Ajax.Utilities;
using Microsoft.Data.SqlClient.Server;
using ProjekatPage.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace ProjekatPage.Controllers
{
    public class StudentController : ApiController
    {
        ///[Authorize(Roles = "admin")]
        [Route("getStudent")]
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            using (var context = new PageBazaContext())
            {
                return context.Student.ToList();
            }
        }

       // [Authorize(Roles = "admin")]
        [Route("removeStudent")]
        [HttpGet]
        public HttpResponseMessage Remove(int id)
        {
            using (var context = new PageBazaContext())
            {
                
               Student student = context.Student.Find(id);
                if (student != null)
                {
                    context.Remove(student);
                    context.SaveChanges();

                    return new HttpResponseMessage(HttpStatusCode.OK);

                }
                 else return new HttpResponseMessage(HttpStatusCode.BadRequest);

            }
        }


        [Authorize(Roles = "admin")]
        [Route("ViewStudent")]
        [HttpGet]
        public Student ViewStudent(int id)
        {
            using (var context = new PageBazaContext())
            {
                var pomocna = context.Student.Where(stud => stud.Studentid == id).ToList();
                Student student = new Student();
                student.Ime = pomocna[0].Ime;
                student.Prezime = pomocna[0].Prezime;
                student.Brojindeksa = pomocna[0].Brojindeksa;
                student.Statusstudentaid = pomocna[0].Statusstudentaid;
                student.Godina = pomocna[0].Godina;
                student.Studentid = pomocna[0].Studentid;

                return student;
            }
        }

        //[Authorize(Roles = "admin")]
        [Route("addStudentPost")]
        [HttpPost]
        public HttpResponseMessage addStudentPost([FromBody]StudentRequestModel student)
        {
            using (var context = new PageBazaContext())
            {
                if (ModelState.IsValid)
                {
                    Student stud = new Student();
                    stud.Ime = student.Ime;
                    stud.Prezime = student.Prezime;
                    stud.Statusstudentaid = student.Statusstudentaid;
                    stud.Godina = student.Godina;
                    stud.Brojindeksa = student.Brojindeksa;

                    context.Add(stud);
                    context.SaveChanges();

                    return new HttpResponseMessage(HttpStatusCode.OK);

                }
                else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

        }

        [Authorize(Roles = "admin")]
        [Route("editStudentPost")]
        [HttpPost]
        public HttpResponseMessage editStudentPost([FromBody]StudentRequestModel student)
        {
            using (var context = new PageBazaContext())
            {
                if (ModelState.IsValid)
                {
                    Student stud = context.Student.Find(student.Studentid);
                    stud.Ime = student.Ime;
                    stud.Prezime = student.Prezime;
                    stud.Statusstudentaid = student.Statusstudentaid;
                    stud.Godina = student.Godina;
                    stud.Brojindeksa = student.Brojindeksa;

                    context.SaveChanges();

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }
      
    }
}
