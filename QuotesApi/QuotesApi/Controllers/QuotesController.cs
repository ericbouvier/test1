using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuotesApi.Data;
using QuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuoteDbContext _quoteDbContext;

        public QuotesController(QuoteDbContext quoteDbContext)
        {
            _quoteDbContext = quoteDbContext;
        }

        // GET: api/<QuotesController>
        [HttpGet]
        public IActionResult Get(string sort)
        {
            IQueryable quotes;

            switch (sort)
            {
                case "desc":
                    quotes = _quoteDbContext.Quotes.OrderByDescending(x => x.Id);
                    break;
                case "asc":
                default:
                    quotes = _quoteDbContext.Quotes.OrderBy(x => x.Id);
                    break;
            }


            return Ok(quotes);
        }

        [HttpGet("[action]")]
        public IActionResult PagingQuotes(int pageNumber = 1, int pageSize = 5)
        {
            return Ok(_quoteDbContext.Quotes.Skip((pageNumber - 1)*pageSize).Take(pageSize));
        }

        [HttpGet("[action]")]
        public IActionResult SearchQuotes(string title)
        {
            return Ok(_quoteDbContext.Quotes.Where(x => x.Title.Contains(title)));
        }

        // GET api/<QuotesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _quoteDbContext.Quotes.Find(id);

            if (entity == null)
            {
                return NotFound("No quote for this ID");
            }

            return Ok(entity);
        }

        // POST api/<QuotesController>
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quoteDbContext.Quotes.Add(quote);
            _quoteDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, "Quote created");
        }

        // PUT api/<QuotesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quoteDbContext.Quotes.Find(id);

            if (entity == null)
            {
                return NotFound("No quote for this ID");
            }

            entity.Author = quote.Author;
            entity.Description = quote.Description;
            entity.Title = quote.Title;
            entity.Type = quote.Type;

            _quoteDbContext.SaveChanges();
            return Ok("Quote updated");
        }

        // DELETE api/<QuotesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var quote = _quoteDbContext.Quotes.Find(id);

            if (quote == null)
            {
                return NotFound("No quote for this ID");
            }

            _quoteDbContext.Quotes.Remove(quote);
            _quoteDbContext.SaveChanges();
            return Ok("Quote deleted");
        }
    }
}
