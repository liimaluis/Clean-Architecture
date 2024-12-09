using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearArchMvc.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        //Faz a configuração da tabela e dos campos, do banco de dados
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Define o ID com chave única
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

            //Definindo a precisão dp preço, 10 posições e 2 casa decimais
            builder.Property(p => p.Price).HasPrecision(10,2);

            //Define o relacionamento de 1 para muitos, 1 categoria pode ter vários produtos sendo a chave estrageria na tabela a CategoryId
            builder.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
