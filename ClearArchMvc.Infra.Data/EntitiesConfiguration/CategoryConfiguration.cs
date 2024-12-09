using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        //Faz a configuração dos campos na tabela do banco de dados
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           //Define o ID com chave única
           builder.HasKey(t => t.Id);

           builder.Property(t => t.Name).HasMaxLength(100).IsRequired();

           // Populando a tabela Category no moento em que ela for gerada
           builder.HasData(
                new Category(1, "Material Escolar"),
                new Category(2, "Eletrônicos"),
                new Category(3, "Acessórios"));

        }
    }
}
