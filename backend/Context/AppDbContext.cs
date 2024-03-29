using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Day> Day { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<User> User { get; set; } 
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Marking> Marking { get; set; }
        public DbSet<EmployeeMarking> EmployeeMarkings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmployeeMarking>()
              //  .HasKey(em => new { em.EmployeeId, em.MarkingId });

            modelBuilder.Entity<EmployeeMarking>()
                .HasOne(em => em.Employee)
                .WithMany(e => e.EmployeeMarkings)
                .HasForeignKey(em => em.EmployeeId);

            modelBuilder.Entity<EmployeeMarking>()
                .HasOne(em => em.Marking)
                .WithMany(m => m.EmployeeMarkings)
                .HasForeignKey(em => em.MarkingId);

            // Configuração dos relacionamentos N para N das entidades Schedule e Day
            modelBuilder.Entity<ScheduleDay>()
                .HasKey(sd => new { sd.ScheduleId, sd.DayId });

            modelBuilder.Entity<ScheduleDay>()
                .HasOne(sd => sd.Schedule)
                .WithMany(s => s.ScheduleDays)
                .HasForeignKey(sd => sd.ScheduleId);

            modelBuilder.Entity<ScheduleDay>()
                .HasOne(sd => sd.Day)
                .WithMany(d => d.ScheduleDays)
                .HasForeignKey(sd => sd.DayId);

            /*/Configuração dos relacionamento N to N das Employee e Marking

            modelBuilder.Entity<EmployeeMarking>()
           .HasKey(em => new { em.EmployeeId, em.MarkingId });

            modelBuilder.Entity<EmployeeMarking>()
                .HasOne(em => em.Employee)
                .WithMany(e => e.EmployeeMarkings)
                .HasForeignKey(em => em.EmployeeId);

            modelBuilder.Entity<EmployeeMarking>()
                .HasOne(em => em.Marking)
                .WithMany(m => m.EmployeeMarkings)
                .HasForeignKey(em => em.MarkingId);

            //Configuração dos relacionamento N to N das entidades Shcedule e Day

            modelBuilder.Entity<ScheduleDay>()
                .HasKey(sd => new { sd.ScheduleId, sd.DayId });

            modelBuilder.Entity<ScheduleDay>()
                .HasOne(sd => sd.Schedule)
                .WithMany(s => s.ScheduleDays)
                .HasForeignKey(sd => sd.ScheduleId);

            modelBuilder.Entity<ScheduleDay>()
                .HasOne(sd => sd.Day)
                .WithMany(d => d.ScheduleDays)
                .HasForeignKey(sd => sd.DayId);*/
        }

    }
}
