using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Data.Context;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class TeacherRepository : CommonValuesForManyViesRepository, IRepository<Teacher>
    {

        public TeacherRepository(UniversityDepartmentWorkloadContext context) : base(context)
        {

        }

        public IEnumerable<Teacher> All => db.Teachers
            .Include(t => t.GenderNavigation)
            .Include(t => t.MaritalStatusNavigation)
            .ToList();

        public IEnumerable<Teacher> FindByName(string teacherName)
        {
            List<Teacher> list = new List<Teacher>();

            list = db.Teachers
                .Include(t => t.GenderNavigation)
                .Include(t => t.MaritalStatusNavigation)
                .Where(t => EF.Functions.Like(t.Name, $"%{teacherName}%"))
                .ToList();
            
            return list;
        }

        public void Add(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
        }

        public void Delete(Teacher teacher)
        {
            if (CheckConstraints(teacher.Id))
            {
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Не можна видалити викладача, так як він викладає предмети.");
            }
        }

        public Teacher FindById(int id)
        {
            Teacher teacher = db.Teachers
                .Include(t => t.GenderNavigation)
                .Include(t => t.MaritalStatusNavigation)
                .Where(t => t.Id == id)
                .First(); 
            
            if(teacher is null) { throw new Exception("Такого викладача не знайдено в базі."); }

            return teacher;
        }

        public void Update(Teacher teacher)
        {
            db.Teachers.Update(teacher);
            db.SaveChanges();
        }

        private bool CheckConstraints(int teacherId)
        {
            bool flag = false;
            if (db.DepartmentWorkloads.Where(dw => dw.TeacherId == teacherId).ToList().Count < 1) { flag = true; }
            return flag;
        }

    }
}
