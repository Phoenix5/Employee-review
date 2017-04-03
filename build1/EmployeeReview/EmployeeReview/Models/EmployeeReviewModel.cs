namespace EmployeeReview
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class EmployeeReviewModel : DbContext
    {
        public EmployeeReviewModel()
            : base("name=EmployeeReview")
        {
        }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeRating> EmployeeRatings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Designation>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Designation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeRating>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeRatings)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.SkillsName)
                .IsUnicode(false);

            modelBuilder.Entity<SkillType>()
                .Property(e => e.SkillTypeName)
                .IsUnicode(false);
        }
    }
}
