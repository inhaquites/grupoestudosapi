using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GrupoEstudos.Domain.Entities;

namespace GrupoEstudos.Infra.Data.EntitiesConfiguration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataCadastro).IsRequired();
        builder.Property(x => x.Nome).HasMaxLength(300).IsRequired();            
        builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Ativo).IsRequired();
        builder.Property(x => x.Senha).HasMaxLength(30).IsRequired();
    }
}
