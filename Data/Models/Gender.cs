using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Teachers = new HashSet<Teacher>();
        }

        public bool Id { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
