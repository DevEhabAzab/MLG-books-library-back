using Microsoft.AspNetCore.Mvc;
using MLG.Models;
using System.Text.Json.Serialization;

namespace MLG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;
        public BookController( LibraryDbContext libraryDbContext)
        {
            _libraryDbContext=libraryDbContext;
        }
        [HttpGet]
        public ActionResult GetBooks() {
            return Ok(_libraryDbContext.Books);
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            if (IsExist(id))
            {
                return Ok( _libraryDbContext.Books.Find(id));
            }
            return NotFound($@"There is no Book With this Id ({id})");
        }

        [HttpPost]
        public ActionResult Create([FromBody]  Book model)
        {
            if (ModelState.IsValid)
            {
                var book=_libraryDbContext.Books.Add(model);
                _libraryDbContext.SaveChanges();
                return Ok( model);
            }
            
            return BadRequest("Cannot Create Book");
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id,[FromBody] Book model)
        {
            if (!IsExist(id)) return NotFound($@"There is no Book With this Id ({id})");

            
            if (ModelState.IsValid)
            {
                var book = _libraryDbContext.Books.Find(id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.PublishedYear = model.PublishedYear;
                book.Genre = model.Genre;
                _libraryDbContext.SaveChanges();

                return Ok(model);
            }
            return BadRequest($@"Cannot Update Book with id ({id})");
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!IsExist(id)) return NotFound($@"There is no Book With this Id ({id})");

            if (ModelState.IsValid)
            {
                var book = _libraryDbContext.Books.Find(id);
                _libraryDbContext.Books.Remove(book);   
                _libraryDbContext.SaveChanges();
                return Ok("Book Deleted Successfully");
            }
            return BadRequest($@"Cannot delete Book with id ({id})");
        }
        private bool IsExist(int id)
        {
            var book=_libraryDbContext.Books.Find(id);
            return book is null? false: true;
        }

    }
}
