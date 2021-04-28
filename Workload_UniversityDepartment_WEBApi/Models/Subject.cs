using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Workload_UniversityDepartment_WEBApi.Models
{
    public partial class Subject
    {
        public Subject()
        {
            
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Назва предмета")]
        [Required(ErrorMessage = "Введіть назву предмета")]
        public string SubjectName { get; set; }
    }
}
