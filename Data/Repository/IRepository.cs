using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Data.Repository
{
    public interface IRepository<TEntity> : ICommonValuesForManyViesRepository, IDisposable where TEntity : class
    {
        IEnumerable<TEntity> All { get; }

        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        TEntity FindById(int id);

        IEnumerable<TEntity> FindByName(string name);
    }

    public interface ICommonValuesForManyViesRepository
    {
        IEnumerable<Gender> GetGenders();
        IEnumerable<MaritalStatus> GetMaritalStatuses();
        IEnumerable<Workload> GetWorkloadTypes();
        IEnumerable<Subject> GetSubjects();
        IEnumerable<Teacher> GetTeachers();
        IEnumerable<DepartmentWorkload> FindWorkloadByTeacherId(int teacherId);
        IEnumerable<DepartmentWorkload> FindWorkloadBySubjectId(int subjectId);
    }
}
