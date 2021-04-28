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
    public class CommonValuesForManyViesRepository : ICommonValuesForManyViesRepository, IDisposable
    {
        protected readonly UniversityDepartmentWorkloadContext db;

        public CommonValuesForManyViesRepository(UniversityDepartmentWorkloadContext context)
        {
            db = context;
        }

        public IEnumerable<Gender> GetGenders()
        {
            return db.Genders.ToList();
        }

        public IEnumerable<MaritalStatus> GetMaritalStatuses()
        {
            return db.MaritalStatuses.ToList();
        }

        public IEnumerable<Workload> GetWorkloadTypes()
        {
            return db.Workloads.ToList();
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return db.Subjects.ToList().OrderBy(s => s.SubjectName);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return db.Teachers.ToList().OrderBy(t => t.Name);
        }

        public IEnumerable<DepartmentWorkload> FindWorkloadByTeacherId(int teacherId)
        {
            List<DepartmentWorkload> list = new List<DepartmentWorkload>();

            list = db.DepartmentWorkloads
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.Workload)
                .Where(dw => dw.TeacherId == teacherId)
                .ToList();
          
            return list;
        }

        public IEnumerable<DepartmentWorkload> FindWorkloadBySubjectId(int subjectId)
        {
            List<DepartmentWorkload> list = new List<DepartmentWorkload>();

            list = db.DepartmentWorkloads
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.Workload)
                .Where(dw => dw.SubjectId == subjectId)
                .ToList();
            return list;
        }

        #region IDisposable
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
