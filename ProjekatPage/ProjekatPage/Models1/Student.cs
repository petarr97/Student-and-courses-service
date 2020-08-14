using System;
using System.Collections.Generic;

namespace ProjekatPage.Models1
{
    public partial class Student
    {
        public int Statusstudentaid { get; set; }
        public int Studentid { get; set; }
        public string Brojindeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int? Godina { get; set; }
    }
}
