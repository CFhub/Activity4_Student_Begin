using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Activity4.DAL;
using Activity4.Entities;

namespace Activity4.BLL
{
    public class StudentController
    {
        public List<Student> List()
        {
            using (var context = new Activity4Context())
            {
                return context.Students.ToList();
            }
        }

        public Student FindByPKID(int studentid)
        {
            using (var context = new Activity4Context())
            {
                return context.Students.Find(studentid);
            }
        }
        public int Update(Student item)
        {
            using (var context = new Activity4Context())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
