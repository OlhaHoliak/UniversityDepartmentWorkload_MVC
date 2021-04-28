using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string MaritalStatusName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
