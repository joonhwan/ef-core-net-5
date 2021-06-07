using System;
using System.Collections.Generic;

#nullable disable

namespace HR.Domain
{
    public partial class EmpDetailsView
    {
        public int EmployeeId { get; set; }
        public string JobId { get; set; }
        public int? ManagerId { get; set; }
        public byte? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public string CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Salary { get; set; }
        public decimal? CommissionPct { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
    }
}
