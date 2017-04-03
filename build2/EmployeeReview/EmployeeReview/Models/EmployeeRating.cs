namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EmployeeRating
    {
        [Key]
        public int EmployeeRatingsID { get; set; }

        public int EmployeeID { get; set; }

        public int? SkillsID { get; set; }

        public int? RatingsID { get; set; }

        [StringLength(2000)]
        public string Comments { get; set; }

        public DateTime CreateDate { get; set; }

        public Employee Employee { get; set; }

        public Rating Rating { get; set; }

        public Skill Skill { get; set; }
    }
}
