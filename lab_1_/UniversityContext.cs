using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab_2_
{
    // 1. СУТНОСТІ (ENTITIES)

    [Table("Positions")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public decimal HourlyRate { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }

    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Workplace { get; set; }
        public int PositionID { get; set; }
        public string HomeAddress { get; set; }
        public string Characteristic { get; set; }

        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }

    [Table("TeacherSubjects")]
    public class TeacherSubject
    {
        public int TeacherID { get; set; }
        public int SubjectID { get; set; }
        public int HoursRead { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }
    }

    // 2. КОНТЕКСТ БАЗИ ДАНИХ (DbContext)

    public class UniversityDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Зчитування рядка підключення з App.config
                string connString = ConfigurationManager.ConnectionStrings["UniversityDb"].ConnectionString;
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування складеного первинного ключа для проміжної таблиці
            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.TeacherID, ts.SubjectID });

            base.OnModelCreating(modelBuilder);
        }
    }
}