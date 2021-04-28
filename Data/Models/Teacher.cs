using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            DepartmentWorkloads = new HashSet<DepartmentWorkload>();
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("ПІБ")]
        [Required(ErrorMessage = "Введіть ПІБ викладача")]
        public string Name { get; set; }


        [DisplayName("Дата народження")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [UIHint("Date")]
        public DateTime? Birthdate { get; set; }

        [DisplayName("Адреса")]
        public string Address { get; set; }


        [DisplayName("Стать")]
        public bool? Gender { get; set; }


        [DisplayName("Сімейний стан")]
        public int? MaritalStatus { get; set; }

        public virtual Gender GenderNavigation { get; set; }
        public virtual MaritalStatus MaritalStatusNavigation { get; set; }
        public virtual ICollection<DepartmentWorkload> DepartmentWorkloads { get; set; }
    }
}
