using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(e => e.IdDoctor);
                e.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                e.Property(e => e.Email).HasMaxLength(100).IsRequired();

                e.ToTable("Doctor");

                e.HasData(new List<Doctor>
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Email = "JanKowalski@o2.pl"
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Franciszek",
                        LastName = "Franciszewski",
                        Email = "Franciszek@o2.pl"
                    }
                });
            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(e => e.IdMedicament);
                e.Property(e => e.Name).HasMaxLength(100).IsRequired();
                e.Property(e => e.Description).HasMaxLength(100).IsRequired();
                e.Property(e => e.Type).HasMaxLength(100).IsRequired();

                e.ToTable("Medicament");

                e.HasData(new List<Medicament> {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Medistenol",
                        Description = "suplement na stawy",
                        Type = "suplement"
                    },
                    new Medicament
                    {
                        IdMedicament = 2,
                        Name = "Apap",
                        Description = "Lek na bol glowy",
                        Type = "Lek przeciwbolowy"
                    }
                });
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(e => e.IdPatient);
                e.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                e.Property(e => e.Birthdate).IsRequired();

                e.ToTable("Patient");

                e.HasData(new List<Patient>
                {
                    new Patient
                    {
                        IdPatient = 1,
                        FirstName = "Pawel",
                        LastName = "Nowak",
                        Birthdate = DateTime.Parse("08-08-2000")
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Jurek",
                        LastName = "Jurkowski",
                        Birthdate = DateTime.Parse("07-07-1990")
                    }
                });
            });

            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(e => e.IdPrescription);
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.DueDate).IsRequired();
                

                e.HasOne(e => e.Doctor)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Patient)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);

                e.ToTable("Prescription");

                e.HasData(new List<Prescription>
                {
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        IdDoctor = 1,
                        IdPatient = 1
                    },
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(15),
                        IdDoctor = 1,
                        IdPatient = 2
                    }
                });
            });

            modelBuilder.Entity<PrescriptionMedicament>(e =>
            {
                e.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                e.Property(e => e.Details).HasMaxLength(100).IsRequired();

                e.HasOne(e => e.Medicament)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Prescription)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);

                e.ToTable("Prescription_Medicament");

                e.HasData(new List<PrescriptionMedicament>
                {
                    new PrescriptionMedicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 1,
                        Dose = 2,
                        Details = "Przyjmowac 2 kapsulki dziennie(rano i wieczorem)"
                    },
                    new PrescriptionMedicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 2,
                        Dose = 4,
                        Details = "Przyjmowac 1 kapsulke co 5-6 godzin, max 4 dziennie"
                    }
                });
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(e => e.IdUser);
                e.Property(e => e.Username).HasMaxLength(100).IsRequired();
                e.Property(e => e.Password).HasMaxLength(100).IsRequired();
                e.Property(e => e.Salt).IsRequired();

                e.ToTable("User");
            });

        }
    }
}
