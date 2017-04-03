namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Skill
    {
        public Skill()
        {
            EmployeeRatings = new HashSet<EmployeeRating>();
        }

        [Key]
        public int SkillsID { get; set; }

        [Required]
        [StringLength(50)]
        public string SkillsName { get; set; }

        public int? SkillTypeID { get; set; }

        public SkillType SkillType { get; set; }

        public ICollection<EmployeeRating> EmployeeRatings { get; set; }
    }
}
