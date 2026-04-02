using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementApi.Data;
using LibraryManagementApi.Models;
using LibraryManagementApi.Dtos;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookResponseDto>>> GetBooks()
            => await _context.Books
                    .Include(b => b.Author)
                    .Select(b => new GetBookResponseDto { Title = b.Title, Author = b.Author!.Name })
                    .ToListAsync();

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookResponseDto>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstAsync(b => b.BookId == id);

            if (book == null)
                return NotFound();

            return new GetBookResponseDto
            {
                Title = book.Title,
                Author = book.Author!.Name
            };
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookDto dto)
        {
            if (id != dto.BookId)
                return BadRequest();
            
            var book = new Book
            {
                BookId = dto.BookId,
                Title = dto.Title ?? throw new NullReferenceException("provide book title"),
                AuthorId = dto.AuthorId
            };
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new { ex.Message });
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(AddNewBookDto dto)
        {
            var book = new Book { Title = dto.Title ?? string.Empty, AuthorId = dto.AuthorId };
            _context.Books.Add(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {}

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
