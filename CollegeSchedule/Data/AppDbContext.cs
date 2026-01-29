using CollegeSchedule.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeSchedule.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Building> Buildings => Set<Building>(); // выполнять запросы к табл и сохранять в них данные
        public DbSet<Classroom> Classrooms => Set<Classroom>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();
        public DbSet<Weekday> Weekdays => Set<Weekday>();
        public DbSet<LessonTime> LessonTimes => Set<LessonTime>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // метод ограничений конфликтов
            // Индексы для Schedul
            modelBuilder.Entity<Schedule>() //одна группа не может находиться на разных парах в одно время в одну дату
            .HasIndex(s => new
            {
                s.LessonDate,
                s.LessonTimeId,
                s.GroupId,
                s.GroupPart
            })
            .IsUnique();
            modelBuilder.Entity<Schedule>() //разные пары в одну аудиторию
            .HasIndex(s => new { s.LessonDate, s.LessonTimeId, s.ClassroomId })
            .IsUnique();
            // Конвертация enum в string
            modelBuilder.Entity<Schedule>()
            .Property(s => s.GroupPart)
            .HasConversion<string>();
        }
    }
}