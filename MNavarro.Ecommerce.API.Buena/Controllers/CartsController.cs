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
    public class CartsController : ControllerBase
    {
        private readonly EcommerceDb _ecommerceDb;

        public CartsController(EcommerceDb ecommerceDb)
        {
            _ecommerceDb = ecommerceDb;
        }

        // GET api/<CartController>/5
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCarts()
        {
            if (_ecommerceDb.Cart.Any())
                return Ok(_ecommerceDb.Cart.Include(x => x.Product));
            else
                return NoContent();
        }

        // GET api/<CartsController>/email
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<Cart>> GetCartByEmail(String email)
        {
            if (_ecommerceDb.Cart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim()))
                return Ok(_ecommerceDb.Cart.Include(x => x.Product).Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()));
            else
                return NoContent();
        }

        // POST api/<CartsController>
        [HttpPost]
        public IActionResult Add([FromBody] Cart cart)
        {
            if (!_ecommerceDb.Cart.Any(x => x.Id == cart.Id))
            {
                _ecommerceDb.Cart.Add(cart);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Exists a cart with id{cart.Id}");
            }
        }

        // PUT api/<CartsController>/email/idProduct
        [HttpPut("{email}/{idProduct}")]
        public IActionResult Update(String email, int idProduct, [FromBody] int cantidad)
        {
            if (_ecommerceDb.Cart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct))
            {
                var CartToUpdate = _ecommerceDb.Cart.Single(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct);
                CartToUpdate.Quantity = cantidad;
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Doesn't exist a cart with email {email} and productId {idProduct}");
            }
        }

        // DELETE api/<CartsController>/email
        [HttpDelete("{email}")]
        public IActionResult DeleteCarts(String email)
        {
            if (_ecommerceDb.Cart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim()))
            {
                var CartsToDelete = _ecommerceDb.Cart.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
                _ecommerceDb.Cart.RemoveRange(CartsToDelete);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Don't exist any cart with email {email}");
            }
        }

        // DELETE api/<CartsController>/email/idProduct
        [HttpDelete("{email}/{idProduct}")]
        public IActionResult DeleteCart(String email, int idProduct)
        {
            if (_ecommerceDb.Cart.Any(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct))
            {
                var CartToDelete = _ecommerceDb.Cart.Single(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Id == idProduct);
                _ecommerceDb.Cart.Remove(CartToDelete);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Doesn't exist a cart with email {email} and productId {idProduct}");
            }
        }
    }
}