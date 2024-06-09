using ISAS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Configurations
{
    public class ISASDbContext : DbContext
    {
        public ISASDbContext(DbContextOptions<ISASDbContext> options): base(options)
        {

        }

        public DbSet<Declaration> Declarations { get; set; } = null!;
        public DbSet<BiologicalTrace> BiologicalTraces { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Victim> Victims { get; set; } = null!;
        public DbSet<Witness> Witnesses { get; set; } = null!;
        public DbSet<Suspect> Suspects { get; set; } = null!;
        public DbSet<Evidence> Evidences { get; set; } = null!;
        public DbSet<PhysicalEvidence> PhysicalEvidences { get; set; } = null!;
        public DbSet<BiologicalEvidence> BiologicalEvidences { get; set; } = null!;
    }
}