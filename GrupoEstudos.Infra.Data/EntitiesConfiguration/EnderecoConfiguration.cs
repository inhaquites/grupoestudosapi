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
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Logradouro).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Numero).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Bairro).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Estado).HasMaxLength(2).IsRequired();

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.Enderecos)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
