using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private Random random = new Random((int)(DateTime.Now.Ticks & (long)int.MaxValue));

        public LibraryController()
        {
        }

        [HttpGet("books")]
        public async Task<string> GetBooksAsync()
        {
            string json = string.Empty;
                            
            // Sleep up to 10 seconds
            await Task.Run(() =>
            {
                Thread.Sleep(random.Next() % 100);

                var books = Library.GetBooks();

                json = JsonSerializer.Serialize(books);
            });

            return json;
        }

        [HttpPost("book")]
        public async Task<string> PostBookAsync(string book)
        {
            string json = string.Empty;

            Action post =
                () =>
                {
                    var newBook = JsonSerializer.Deserialize<LibraryBook>(book);
                    if (newBook != null)
                    {
                        newBook = Library.AddBook(newBook);
                    }
                    json = JsonSerializer.Serialize(newBook);
                };

            await Task.Run(post);
            return json;
        }

    }
}
