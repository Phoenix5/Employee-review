namespace EmployeeReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SkillType")]
    public class SkillType
    {
        public int SkillTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SkillTypeName { get; set; }
    }
}
