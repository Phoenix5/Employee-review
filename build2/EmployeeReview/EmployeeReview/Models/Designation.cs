namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Designation")]
    public class Designation
    {
       
        public Designation()
        {
            Employees = new HashSet<Employee>();
        }

        public int DesignationID { get; set; }

        [Column("Designation")]
        [Required]
        [StringLength(30)]
        public string Designation1 { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
