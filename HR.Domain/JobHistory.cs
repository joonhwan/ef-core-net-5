using System;
using System.Collections.Generic;

#nullable disable

namespace HR.Domain
{
    public partial class JobHistory
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobId { get; set; }
        public byte? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Job Job { get; set; }
    }
}
