using System;
using System.Collections.Generic;

namespace QLSV.Entities
{
    public partial class Assessment
    {
        public int StudentID { get; set; }
        public string StudentName{ get; set; } = null!;
        public string FacultyName { get; set; } = null!;
        public string? ClassName { get; set; }
        public double FinalGrade { get; set; }
        public string ScoreRating { get; set; } = null!;

        

    }
}
