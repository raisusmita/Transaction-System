using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace Server.Data
{

    public class AppDbContext : System.Data.Entity.DbContext
    {
        
        public AppDbContext():base("name=AqoreDb")
        {
            {
                this.Configuration.LazyLoadingEnabled = false;
                this.Configuration.ProxyCreationEnabled = false;
            }

        }


        public System.Data.Entity.DbSet<Products> Products { get; set; }
        public System.Data.Entity.DbSet<Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Invoice> Invoices { get; set; }

        // public object SalesTransactions { get; internal set; }

        public System.Data.Entity.DbSet<SalesTransaction> SalesTransactions { get; set; }

       /** public System.Data.Entity.DbSet<SalesTransaction> GetSalesTransactions()
        {
            return salesTransactions;
        }

        public void SetSalesTransactions(System.Data.Entity.DbSet<SalesTransaction> value)
        {
            salesTransactions = value;
        }**/

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SalesTransaction>()
               // .HasKey(st => new { st.ProductId, st.CustomerId });
            modelBuilder.Entity<SalesTransaction>()
                .HasRequired(st => st.Product)
                .WithMany(p => p.SalesTransactions)
                .HasForeignKey(st => st.ProductId);
            modelBuilder.Entity<SalesTransaction>()
                .HasRequired(st => st.Customer)
                .WithMany(c => c.SalesTransactions)
                .HasForeignKey(st => st.CustomerId);
            modelBuilder.Entity<SalesTransaction>()
               .HasOptional(st => st.Invoice)
               .WithMany(i => i.SalesTransactions)
               .HasForeignKey(st => st.InvoiceId);
           /** modelBuilder.Entity<Invoice>()
               .Property(i => i.InvoiceId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);**/
        }

        

    }
}