using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace Server.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public bool CustomerGender { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must be of 10 digits.")]
        public string CustomerPhone { get; set; }

        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}