using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Activity4.Entities
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public double Credits { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public int ProgramID { get; set; }
    }
}
