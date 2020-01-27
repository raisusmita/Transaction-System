using Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class SalesTransaction
    {

        [Key]
        public int SalesTranId { get; set; }
        // [Column(Order=1)]
        //[Required]
        public int ProductId { get; set; }

        //[Key]
        //[Column(Order = 2)]
        //[Required]
        public int CustomerId { get; set; }

        //[DefaultValue("0")]
        public int? InvoiceId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string CustomerName { get; set; }

        //[Required]
        public int Rate { get; set; }

        [Required]
        public int Quantity { get; set; }

       // [Required]
        public int Total { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual Products Product { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Invoice Invoice { get; set; }





    }
}