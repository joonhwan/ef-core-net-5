using System;
using System.Collections.Generic;

#nullable disable

namespace HR.Domain
{
    public partial class Employee
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            InverseManager = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string JobId { get; set; }
        public decimal? Salary { get; set; }
        public decimal? CommissionPct { get; set; }
        public int? ManagerId { get; set; }
        public byte? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Job Job { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}
