using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPI.Models;
using System.Data.Entity;

namespace Server.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDesc { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }

    }
}