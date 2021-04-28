using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Workload_UniversityDepartment_WEBApi.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("ПІБ")]
        [Required(ErrorMessage = "Введіть ПІБ викладача")]
        public string Name { get; set; }
        [DisplayName("Дата народження")]
        public string Birthdate { get; set; }
        [DisplayName("Адреса")]
        public string Address { get; set; }
        [DisplayName("Стать")]
        public string Gender { get; set; }
        [DisplayName("Сімейний стан")]
        public string MaritalStatus { get; set; }
    }
}
