using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Activity4.DAL;
using Activity4.Entities;
using System.ComponentModel;

namespace Activity4.BLL
{
    public class ProgramController
    {
        public List<Program> List()
        {
            using (var context = new Activity4Context())
            {
                return context.Programs.ToList();
            }
        }
    }
}
