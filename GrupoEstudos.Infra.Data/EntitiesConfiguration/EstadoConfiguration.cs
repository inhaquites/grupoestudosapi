using GrupoEstudos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Infra.Data.EntitiesConfiguration
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Sigla)
                .HasColumnName("Sigla")
                .HasMaxLength(2)
                .IsRequired();

            builder.HasMany(x => x.Municipios)
                .WithOne(x => x.Estado)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
