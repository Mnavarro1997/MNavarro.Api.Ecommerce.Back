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
    public class CategoriesController : ControllerBase
    {
        private readonly EcommerceDb _ecommerceDb;

        public CategoriesController(EcommerceDb ecommerceDb)
        {
            _ecommerceDb = ecommerceDb;
        }

        // GET api/<CategoryController>/5
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            if (_ecommerceDb.Category.Any())
                return Ok(_ecommerceDb.Category);
            else
                return NoContent();
        }

        // GET api/<CategoriesController>/categoryId
        [HttpGet("{id}")]
        public ActionResult <Category> GetCategoryById(int id)
        {
            if (_ecommerceDb.Category.Any(x => x.Id == id))
                return Ok(_ecommerceDb.Category.FirstOrDefault(x => x.Id == id));
            else
                return NoContent();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!_ecommerceDb.Category.Any(x => x.Id == category.Id))
            {
                _ecommerceDb.Category.Add(category);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Exists a category with category{category.Id}");
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var CategoryToUpdate = _ecommerceDb.Category.Single(x => x.Id == id);
            _ecommerceDb.Category.Remove(CategoryToUpdate);
            _ecommerceDb.Category.Add(category);
            _ecommerceDb.SaveChanges();
            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_ecommerceDb.Category.Any(x => x.Id == id))
            {
                var CategoryToDelete = _ecommerceDb.Category.Single(c => c.Id == id);
                _ecommerceDb.Category.Remove(CategoryToDelete);
                _ecommerceDb.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"Don't exist a category with id {id}");
            }
        }
    }
}