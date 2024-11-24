using Contatos.SharedKernel.Entities;
using Contatos.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contatos.Cadastro.Api.Infra.Data.Mappings;

public class ContatoMapping : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Nome, tf =>
        {
            tf.Property(c => c.PrimeiroNome)
                .HasColumnName("PrimeiroNome")
                .IsRequired()
                .HasColumnType("varchar(60)");

            tf.Property(c => c.Sobrenome)
                .HasColumnName("Sobrenome")
                .IsRequired()
                .HasColumnType("varchar(60)");
        });

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Endereco)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(Email.MaxLength)
                .HasColumnType($"varchar({Email.MaxLength})");
        });

        builder.OwnsMany(c => c.Telefones, tf =>
        {
            tf.WithOwner().HasForeignKey("ContatoId");

            tf.HasKey("ContatoId", "Ddd", "Numero", "Tipo");

            tf.Property(c => c.Ddd)
                .IsRequired()
                .HasColumnType("int");

            tf.Property(c => c.Numero)
                .IsRequired()
                .HasColumnType("varchar(10)");

            tf.Property(c => c.Tipo)
                .IsRequired()
                .HasColumnType("int");

            tf.ToTable("Telefones");
        });
    }
}