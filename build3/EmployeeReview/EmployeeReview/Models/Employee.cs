namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Employee
    {
        public Employee()
        {
         //   EmployeeRatings = new HashSet<EmployeeRating>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(30)]
        public string EmployeeName { get; set; }

        public int DesignationID { get; set; }

        public Designation Designation { get; set; }

        public ICollection<EmployeeRating> EmployeeRatings { get; set; }
    }
}
