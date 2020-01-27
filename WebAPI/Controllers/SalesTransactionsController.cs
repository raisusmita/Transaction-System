using Server.Data;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCors("http://localhost:4200", "*", "*")]
    public class SalesTransactionsController : ApiController
    {
            [HttpGet]
            public IHttpActionResult GetSalesTransactions()
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                    var salestrans = context.SalesTransactions.ToList();
                        return Ok(salestrans);
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }

        [HttpGet]
        public IHttpActionResult GetSalesTransaction(int id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var salestran = context.SalesTransactions.FirstOrDefault(n => n.SalesTranId == id);
                    if (salestran == null) return NotFound();
                    return Ok(salestran);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IHttpActionResult PostSalesTransaction([FromBody] SalesTransaction salesTransaction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                using (var context = new AppDbContext())
                {
                    //Getting product having the same name that is passed from the form
                    var product = context.Products.FirstOrDefault(n => n.ProductName == salesTransaction.ProductName);

                    //Getting customer having the same name that is passed from the form
                    var customer = context.Customers.FirstOrDefault(n => n.CustomerName == salesTransaction.CustomerName);

                    //Not found message if null
                    if (product == null) return NotFound();
                    if (customer == null) return NotFound();

                    //Assign ProductId based on ProductName
                    salesTransaction.ProductId = product.ProductId;

                    //Assign CustomerId based on CustomerName
                    salesTransaction.CustomerId = customer.CustomerId;

                    salesTransaction.Rate = product.Rate;
                    salesTransaction.Total = product.Rate * salesTransaction.Quantity;
                    //salesTransaction.InvoiceId = 3;
                    context.SalesTransactions.Add(salesTransaction);
                    context.SaveChanges();
                    return Ok("Sales Transaction was created!");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

       [HttpPut]
        public IHttpActionResult UpdateSalesTransaction(int id, [FromBody] SalesTransaction salesTransaction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != salesTransaction.SalesTranId) return BadRequest();

            try
            {
                using (var context = new AppDbContext())
                {
                    //Finding old data using id
                    var oldTransaction = context.SalesTransactions.FirstOrDefault(n => n.SalesTranId == id);
                   
                    if (oldTransaction == null) return NotFound();

                    oldTransaction.ProductName = salesTransaction.ProductName;
                    oldTransaction.CustomerName = salesTransaction.CustomerName;

                    //Getting product having the same name that is passed from the form
                    var product = context.Products.FirstOrDefault(n => n.ProductName == oldTransaction.ProductName);

                    //Getting customer having the same name that is passed from the form
                    var customer = context.Customers.FirstOrDefault(n => n.CustomerName == oldTransaction.CustomerName);

                    //Not found message if null
                    if (product == null) return NotFound();
                    if (customer == null) return NotFound();

                    //Replacing old data with the new one
                    oldTransaction.ProductId = product.ProductId;
                    oldTransaction.CustomerId = customer.CustomerId;
                    oldTransaction.Quantity = salesTransaction.Quantity;
                    oldTransaction.Rate = salesTransaction.Rate;
                    oldTransaction.Total = oldTransaction.Quantity * oldTransaction.Rate;
                    oldTransaction.Status = salesTransaction.Status;
                    oldTransaction.InvoiceId = salesTransaction.InvoiceId;

                    context.SaveChanges();
                    return Ok("Sales Transaction Updated!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var transaction = context.SalesTransactions.FirstOrDefault(n => n.SalesTranId == id);
                    if (transaction == null) return NotFound();

                    context.SalesTransactions.Remove(transaction);
                    context.SaveChanges();
                    return Ok("Sales Transaction Deleted!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
