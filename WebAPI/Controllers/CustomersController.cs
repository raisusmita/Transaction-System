using Server.Data;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.Controllers
{
    [EnableCors("http://localhost:4200", "*", "*")]
    public class CustomersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var customers = context.Customers.ToList();
                    return Ok(customers);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        public IHttpActionResult GetCUstomer(int id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var customer = context.Customers.FirstOrDefault(n => n.CustomerId == id);
                    if (customer == null) return NotFound();
                    return Ok(customer);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                using (var context = new AppDbContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    return Ok("Customer was created!");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != customer.CustomerId) return BadRequest();

            try
            {
                using (var context = new AppDbContext())
                {
                    //Finding old data using id
                    var oldCustomer = context.Customers.FirstOrDefault(n => n.CustomerId == id);

                    if (oldCustomer == null) return NotFound();

                    oldCustomer.CustomerName = customer.CustomerName;
                    oldCustomer.CustomerGender = customer.CustomerGender;
                    oldCustomer.CustomerAddress = customer.CustomerAddress;
                    oldCustomer.CustomerEmail = customer.CustomerEmail;
                    oldCustomer.CustomerPhone = customer.CustomerPhone;

                    context.SaveChanges();
                    return Ok("Customer Updated!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var customer = context.Customers.FirstOrDefault(n => n.CustomerId == id);
                    if (customer == null) return NotFound();

                    context.Customers.Remove(customer);
                    context.SaveChanges();
                    return Ok("Customer Deleted!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
