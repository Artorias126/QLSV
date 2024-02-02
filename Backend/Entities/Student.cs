using System;
using System.Collections.Generic;

namespace QLSV.Entities
{
    public partial class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public string FacultyName { get; set; } = null!;

       
    }
}
