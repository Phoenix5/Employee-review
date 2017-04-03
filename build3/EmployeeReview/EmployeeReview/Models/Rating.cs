namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Rating
    {
       public Rating()
        {
         //   EmployeeRatings = new HashSet<EmployeeRating>();
        }

        [Key]
        public int RatingsID { get; set; }

        [Required]
        [StringLength(30)]
        public string RatingsName { get; set; }

        public int? SkillTypeID { get; set; }

        public SkillType SkillType { get; set; }

        public ICollection<EmployeeRating> EmployeeRatings { get; set; }
    }
}
