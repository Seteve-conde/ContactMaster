using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
        }
    }
}
