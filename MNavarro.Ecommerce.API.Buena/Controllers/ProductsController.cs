using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNavarro.Ecommerce.API.Buena.DataBase;
using MNavarro.Ecommerce.API.Buena.Models;

namespace MNavarro.Ecommerce.API.Buena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EcommerceDb _ecommerceDb;

        public ProductsController(EcommerceDb ecommerceDb)
        {
            _ecommerceDb = ecommerceDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            if (_ecommerceDb.Product.Any())
                return Ok(_ecommerceDb.Product);
            else
                return NoContent();
        }

        // GET api/<ProductsController>/productId
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            if (_ecommerceDb.Product.Any(x => x.Id == id))
                return Ok(_ecommerceDb.Product.FirstOrDefault(x => x.Id == id));
            else
                return NoContent();
        }

        // GET api/<ProductsController>/categoryId
        [HttpGet("category/{id}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            if (_ecommerceDb.Product.Any(x => x.CategoryId == id))
                return Ok(_ecommerceDb.Product.Where(x => x.CategoryId == id));
            else
                return NoContent();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            if (!_ecommerceDb.Product.Any(p => p.Id == product.Id))
            {
                _ecommerceDb.Product.Add(product);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Exists a product with id{product.Id}");
            }
        }

        // PUT api/<ProductsController>/productId
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (_ecommerceDb.Product.Any(p => p.Id == id))
            {
                var ProductToUpdate = _ecommerceDb.Product.Single(p => p.Id == id);
                _ecommerceDb.Product.Remove(ProductToUpdate);
                _ecommerceDb.Product.Add(product);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Don't exist a product with id {id}");
            }
        }

        // DELETE api/<ProductsController>/productId
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_ecommerceDb.Product.Any(p => p.Id == id))
            {
                var ProductToDelete = _ecommerceDb.Product.Single(p => p.Id == id);
                _ecommerceDb.Product.Remove(ProductToDelete);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Don't exist a product with id {id}");
            }
        }
    }
}