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
    public class SubjectRepository : CommonValuesForManyViesRepository, IRepository<Subject>
    {

        public SubjectRepository(UniversityDepartmentWorkloadContext context) : base(context)
        {

        }


        public IEnumerable<Subject> All => db.Subjects.Include(t => t.DepartmentWorkloads).ToList();

        public IEnumerable<Subject> FindByName(string subjectName)
        {
            List<Subject> list = new List<Subject>();

            list = db.Subjects.Where(s => s.SubjectName == subjectName).ToList();

            return list;
        }

        public void Add(Subject subject)
        {
            if (CheckForDoubl(subject.SubjectName))
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Такий предмет вже існує.");
            }
        }

        public void Delete(Subject subject)
        {
            if (CheckConstraints(subject.Id))
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Не можна видалити предмет, так як він викладається.");
            }
        }

        public Subject FindById(int id)
        {
            Subject subject = db.Subjects
                .Include(t => t.DepartmentWorkloads)
                .Where(s => s.Id == id)
                .First();

            return subject;
        }

        public void Update(Subject subject)
        {
            if (CheckForDoubl(subject.SubjectName))
            {
                Subject updSubject = db.Subjects
                    .Where(s => s.Id == subject.Id)
                    .First();
                if (updSubject is null) { throw new Exception("Предмет, що змінюється не знайдено в базі!"); }
                updSubject.SubjectName = subject.SubjectName;
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Предмет з такою назвою вже існує.");
            }
        }

        private bool CheckConstraints(int subjectId)
        {
            bool flag = false;

            if (db.DepartmentWorkloads.Where(dw => dw.SubjectId == subjectId).ToList().Count < 1)
            //if (db.Subjects.Include(t => t.DepartmentWorkloads).Where(s => s.Id == subjectId).First().DepartmentWorkloads.Count < 1)
            {
                flag = true;
            }

            return flag;
        }

        private bool CheckForDoubl(string name)
        {
            bool flag = false;
            if (db.Subjects.Where(s => s.SubjectName == name).ToList().Count < 1)
            {
                flag = true;
            }
            return flag;
        }

    }
}
