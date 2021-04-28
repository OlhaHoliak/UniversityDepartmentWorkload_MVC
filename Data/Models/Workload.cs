using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Workload
    {
        public Workload()
        {
            DepartmentWorkloads = new HashSet<DepartmentWorkload>();
        }

        public int Id { get; set; }
        public string WorkloadName { get; set; }

        public virtual ICollection<DepartmentWorkload> DepartmentWorkloads { get; set; }
    }
}
