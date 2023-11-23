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
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(300).IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(11);
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.Telefone).HasMaxLength(11);

            builder.HasMany(x => x.Enderecos)
                .WithOne(x => x.Pessoa)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
