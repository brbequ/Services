using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookStoreController : ControllerBase
    {
        private BookStore bookStore;
        private Random random;

        public BookStoreController()
        {
            bookStore = new BookStore();
            random = new Random((int)(DateTime.Now.Ticks & (long)int.MaxValue));
        }

        [HttpGet("books")]
        public async Task<string> GetInventory()
        {
            string json = string.Empty;

            await Task.Run(() =>
            {
                Thread.Sleep(random.Next() % 2500);
                var books = bookStore.GetInventory();
                json = JsonSerializer.Serialize(books);
            });

            return json;
        }
    }
}
