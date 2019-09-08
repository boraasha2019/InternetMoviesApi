using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InternetMoviesOnDemand.Entities;
using InternetMoviesOnDemand.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternetMoviesOnDemand.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        /// <summary>
        /// To get all the categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return TemporaryDataContext._category;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = TemporaryDataContext._category.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("Category with id=" + id.ToString() + " not found.");
            }
            else
            {
                return Ok(category);
            }
        }

        // POST api/values
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Add(Category category)
        {
            var count = TemporaryDataContext._category.Count();
            var cat = TemporaryDataContext._category;
            if (!cat.Where(x => x.CategoryName == category.CategoryName).Any())
            {
                cat.Add(new Category { CategoryName = category.CategoryName, Id = count + 1 });
                return Ok("Category Successfully Added!!");
            }

            return BadRequest("Category already exist!!");
        }
     
        // DELETE api/values/5
        [Authorize(Roles ="Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = TemporaryDataContext._category.FirstOrDefault(e => e.Id == id);
            if (category == null)
            {
                return NotFound("Category with id=" + id.ToString() + "not found to delete.");
            }
            else
            {
                TemporaryDataContext._category.Remove(category);
                return Ok();
            }
        }
    }
}
