using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public partial class DepartmentWorkload
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Викладач")]
        public int? TeacherId { get; set; }

        [DisplayName("Предмет")]
        public int? SubjectId { get; set; }

        [DisplayName("Тип навантаження")]
        public int? WorkloadId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Години")]
        [Required(ErrorMessage = "Введіть кількість годин (більшу за 0)")]
        [Range(typeof(int), "1", "9999999", ErrorMessage = "Введіть додатне значення")]
        public int? WorkHours { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Workload Workload { get; set; }
    }
}
