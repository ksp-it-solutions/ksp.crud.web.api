using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.DataModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
        public DbSet<Beneficiaries> Beneficiaries { get; set; }
        public DbSet<Token> Token { get; set; }
    }
}
