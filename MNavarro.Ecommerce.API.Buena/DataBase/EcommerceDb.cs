using Microsoft.EntityFrameworkCore;
using MNavarro.Ecommerce.API.Buena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNavarro.Ecommerce.API.Buena.DataBase
{
    public partial class EcommerceDb : DbContext
    {
        public DbSet<Cart> Cart { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        public EcommerceDb(DbContextOptions<EcommerceDb> options)
            : base(options)
        {
        }
    }
}