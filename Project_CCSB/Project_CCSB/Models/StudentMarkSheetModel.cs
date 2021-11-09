using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public class StudentMarkSheetModel
    {
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string Email { get; set; }
        public string SubjectSQL { get; set; }
        public decimal SQLMarks { get; set; }
        public string SubjectEnglish { get; set; }
        public decimal EnglishMarks { get; set; }
        public string SubjectC { get; set; }
        public decimal CMarks { get; set; }
    }
}
