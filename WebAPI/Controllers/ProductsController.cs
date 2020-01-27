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
    [EnableCors ("http://localhost:4200", "*","*")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var products = context.Products.ToList();
                    return Ok(products);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var product = context.Products.FirstOrDefault(n=>n.ProductId ==id);
                    if (product == null) return NotFound();
                    return Ok(product);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PostProduct([FromBody] Products product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                using (var context = new AppDbContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    return Ok("Product was created!");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct (int id, [FromBody] Products product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != product.ProductId) return BadRequest();

            try
            {
                using (var context = new AppDbContext())
                {
                    //Finding old data using id
                    var oldProduct = context.Products.FirstOrDefault(n => n.ProductId == id);
                   
                    if(oldProduct == null) return NotFound();

                    oldProduct.ProductName = product.ProductName;
                    oldProduct.ProductDesc = product.ProductDesc;
                    oldProduct.Quantity = product.Quantity;
                    oldProduct.Rate = product.Rate;

                    context.SaveChanges();
                    return Ok("Product Updated!");
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
                    var product = context.Products.FirstOrDefault(n => n.ProductId == id);
                    if (product == null) return NotFound();

                    context.Products.Remove(product);
                    context.SaveChanges();
                    return Ok("Product Deleted!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
