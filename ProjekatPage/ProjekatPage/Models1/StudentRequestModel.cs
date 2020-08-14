using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjekatPage.Models1
{
    public class StudentRequestModel
    {
        public int Statusstudentaid { get; set; }
        public int Studentid { get; set; }
        public string Brojindeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
    }
}