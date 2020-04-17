using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Activity4.Entities
{
    [Table("Program")]
    public class Program
    {
        [Key]
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public decimal Tuition { get; set; }
    }
}
