using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;


        public BookRepository(BookStoreContext context)
        {

            _context = context;
        }
        
        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            {
                Key = x.Id,
                Value = x.Title,
            }).FirstOrDefaultAsync();
            return records;
        }
        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Value,
                
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;

        }
        public async Task UpdateBookAsync(int bookId,BookModel bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if(book != null)
            {
                book.Title = bookModel.Value;
                
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() { Id = bookId };

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

        }
    }
}
