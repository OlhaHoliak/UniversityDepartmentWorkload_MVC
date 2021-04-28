using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public partial class Subject
    {
        public Subject()
        {
            DepartmentWorkloads = new HashSet<DepartmentWorkload>();
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Назва предмету")]
        [Required(ErrorMessage = "Введіть назву предмету")]
        public string SubjectName { get; set; }

        public virtual ICollection<DepartmentWorkload> DepartmentWorkloads { get; set; }
    }
}
