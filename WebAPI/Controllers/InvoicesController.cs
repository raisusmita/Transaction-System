using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class InvoicesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetInvoices()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var invoices = context.Invoices.ToList();
                    return Ok(invoices);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PostInvoice([FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                using (var context = new AppDbContext())
                {
                    
                    //salesTransaction.InvoiceId = 3;
                    context.Invoices.Add(invoice);
                    context.SaveChanges();
                    return Ok(invoice.InvoiceId);


                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
