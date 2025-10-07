using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Markey.Persistance.Data.Tables;

namespace Markey.Persistance.Data.Seeds;

public class OccupationSeed : IEntityTypeConfiguration<Occupation>
{
    public void Configure(EntityTypeBuilder<Occupation> builder)
    {
        builder.HasData(
                new Occupation { Id = Guid.NewGuid(), Name = "Piloto", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7445) },
                new Occupation { Id = Guid.NewGuid(), Name = "Médico", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7446) },
                new Occupation { Id = Guid.NewGuid(), Name = "Ingeniero", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7447) },
                new Occupation { Id = Guid.NewGuid(), Name = "Abogado", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7448) },
                new Occupation { Id = Guid.NewGuid(), Name = "Profesor", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7449) },
                new Occupation { Id = Guid.NewGuid(), Name = "Enfermero", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7450) },
                new Occupation { Id = Guid.NewGuid(), Name = "Arquitecto", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7451) },
                new Occupation { Id = Guid.NewGuid(), Name = "Carpintero", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7452) },
                new Occupation { Id = Guid.NewGuid(), Name = "Electricista", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7453) },
                new Occupation { Id = Guid.NewGuid(), Name = "Mecánico", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7454) },
                new Occupation { Id = Guid.NewGuid(), Name = "Chef", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7455) },
                new Occupation { Id = Guid.NewGuid(), Name = "Panadero", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7456) },
                new Occupation { Id = Guid.NewGuid(), Name = "Diseñador Gráfico", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7457) },
                new Occupation { Id = Guid.NewGuid(), Name = "Programador", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7458) },
                new Occupation { Id = Guid.NewGuid(), Name = "Periodista", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7459) },
                new Occupation { Id = Guid.NewGuid(), Name = "Actor", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7460) },
                new Occupation { Id = Guid.NewGuid(), Name = "Músico", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7461) },
                new Occupation { Id = Guid.NewGuid(), Name = "Fotógrafo", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7462) },
                new Occupation { Id = Guid.NewGuid(), Name = "Policía", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7463) },
                new Occupation { Id = Guid.NewGuid(), Name = "Bombero", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7464) },
                new Occupation { Id = Guid.NewGuid(), Name = "Militar", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7465) },
                new Occupation { Id = Guid.NewGuid(), Name = "Contador", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7466) },
                new Occupation { Id = Guid.NewGuid(), Name = "Dentista", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7467) },
                new Occupation { Id = Guid.NewGuid(), Name = "Veterinario", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7468) },
                new Occupation { Id = Guid.NewGuid(), Name = "Psicólogo", CreationDate = new DateTime(2025, 10, 03, 0, 0, 0, 000, DateTimeKind.Utc).AddTicks(7469) }
        );
    }
}
