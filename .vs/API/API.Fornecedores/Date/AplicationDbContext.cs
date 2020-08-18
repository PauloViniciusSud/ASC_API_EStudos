using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Fornecedores.Date
{
    public class AplicationDbContext : IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        { }

        public AplicationDbContext()
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Data Source = localhost\\sqlexpress; Initial Catalog = FornecedoresDb; Integrated Security = True;");
        }

    }
}
