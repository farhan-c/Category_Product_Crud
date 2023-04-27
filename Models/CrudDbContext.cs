using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Crud_Operation.Models
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext() : base("Crud_Operation")
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserRole> Roles { get; set; }

    }
}