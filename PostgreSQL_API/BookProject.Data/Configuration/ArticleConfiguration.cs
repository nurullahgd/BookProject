using System;
using System.Collections.Generic;
using BookProject.Data.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookProject.Data.Configuration
{
    public class ArticleConfiguration
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(s => s.Title).HasMaxLength(300);
            builder.Property(s => s.Content).HasMaxLength(600);
        }
    }
}
