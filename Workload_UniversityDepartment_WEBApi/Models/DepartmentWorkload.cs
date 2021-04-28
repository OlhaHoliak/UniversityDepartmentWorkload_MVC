using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Workload_UniversityDepartment_WEBApi.Models
{
    public class DepartmentWorkload
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int TeacherId { get; set; }
        [DisplayName("ПІБ")]
        [Required(ErrorMessage = "Введіть ПІБ викладача")]
        public string Teacher { get; set; }

        public int SubjectId { get; set; }
        [DisplayName("Предмет")]
        [Required(ErrorMessage = "Введіть назву предмету")]
        public string Subject { get; set; }

        [DisplayName("Навантаження")]
        [Required(ErrorMessage = "Введіть тип навантаження")]
        public string Workload { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Години")]
        [Required(ErrorMessage = "Введіть кількість годин (більшу за 0)")]
        [Range(typeof(int), "1", "9999999", ErrorMessage = "Введіть додатне значення")]
        public int? WorkHours { get; set; }
    }
}
