using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public int Tax { get; set; }
        [Required]
        public int GrandTotal { get; set; }
        // [Required]
        //public DateTime DateTime { get; set; }

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }

    }
}