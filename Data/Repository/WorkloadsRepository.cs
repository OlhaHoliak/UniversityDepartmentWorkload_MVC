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
    public class WorkloadsRepository : CommonValuesForManyViesRepository, IRepository<DepartmentWorkload>
    {

        public WorkloadsRepository(UniversityDepartmentWorkloadContext context) : base(context)
        {
            
        }

        public IEnumerable<DepartmentWorkload> All => db.DepartmentWorkloads
            .Include(t => t.Subject)
            .Include(t => t.Teacher)
            .Include(t => t.Workload)
            .ToList();

        

        public IEnumerable<DepartmentWorkload> FindByName(string subjectName)
        {
            List<DepartmentWorkload> list = new List<DepartmentWorkload>();

            list = db.DepartmentWorkloads
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .Include(t => t.Workload)
                .Where(dw => dw.Subject.SubjectName == subjectName)
                .ToList();

            return list;
        }

        public void Add(DepartmentWorkload entity)
        {
            db.DepartmentWorkloads.Add(entity);
            db.SaveChanges();
        }

        public void Delete(DepartmentWorkload entity)
        {
            db.DepartmentWorkloads.Remove(entity);
            db.SaveChanges();
        }

        public DepartmentWorkload FindById(int id)
        {
            DepartmentWorkload entity = db.DepartmentWorkloads
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .Include(t => t.Workload)
                .Where(dw => dw.Id == id)
                .First();

            if (entity is null) { throw new Exception("Запис з таким номером не знайдено!"); }

            return entity;
        }

        public void Update(DepartmentWorkload entity)
        {
            db.DepartmentWorkloads.Update(entity);
            db.SaveChanges();
        }
    }
}
