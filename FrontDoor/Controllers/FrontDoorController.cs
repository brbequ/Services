using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontDoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrontDoorController : ControllerBase
    {
        // These values should be dynamically loaded from configuration
        const string libraryAddress = "https://localhost:7283/Library/";
        const string bookStoreAddress = "https://localhost:7111/BookStore/";

        // Construct the HttpClients once and reuse them
        private static HttpClient libraryClient = new HttpClient()
        {
            BaseAddress = new Uri(libraryAddress),
        };

        private static HttpClient bookStoreClient = new HttpClient()
        {
            BaseAddress = new Uri(bookStoreAddress),
        };

        private readonly ILogger<FrontDoorController> _logger;

        public FrontDoorController(ILogger<FrontDoorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("books")]
        public async Task<string> GetBooksAsync()
        {
            GetBooksResponse response = new();
            var tasks = new List<Task<HttpResponseMessage>>();

            try
            {
                // GetAsync fails when the default header includes content type.
                // Manually construct the request message and send it with SendAsync.
                HttpRequestMessage libraryMsg = new HttpRequestMessage();
                libraryMsg.Method = HttpMethod.Get;
                libraryMsg.RequestUri = new Uri(new Uri(libraryAddress), "books");
                libraryMsg.Headers.Clear();
                // Add auth back to the header for calls to the backend.
                
                tasks.Add(libraryClient.SendAsync(libraryMsg));

                HttpRequestMessage bookStoreMsg = new HttpRequestMessage();
                bookStoreMsg.Method = HttpMethod.Get;
                bookStoreMsg.RequestUri = new Uri(new Uri(bookStoreAddress), "books");
                bookStoreMsg.Headers.Clear();
                // Add auth back to the header for calls to the backend

                tasks.Add(bookStoreClient.SendAsync(bookStoreMsg));

                // Wait for the tasks to complete successfully
                await Task.WhenAll(tasks);
                tasks.ForEach(task => task.Result.EnsureSuccessStatusCode());
            }
            catch (HttpRequestException)
            {
                // Return an empty response
                return JsonSerializer.Serialize(response);
            }

            // Process the content
            int index = 0;
            string content = string.Empty;
            foreach (var task in tasks)
            {
                content = await task.Result.Content.ReadAsStringAsync();

                // Remove the enclosing quotes before assigning it to the response
                content = content.Substring(1, content.Length - 2);
                switch (index)
                {
                    case 0:
                        response.libraryBooks = content;
                        break;
                    case 1:
                        response.bookStoreInventory = content;
                        break;
                }
                index++;
            }
            
            // Serialize the result
            var result = JsonSerializer.Serialize(response);

            // Unescape the excessive escaping done by the serializer.
            return Regex.Unescape(result);
        }

        [HttpPost("book")]
        public async Task<string> PostLibraryBookAsync(string ISBN, string author, string title, string description, int quantity)
        {
            LibraryBook book = new(ISBN, author, title, description, quantity);
            string bookJson = JsonSerializer.Serialize(book);

            var response = await libraryClient.PostAsJsonAsync(new Uri(new Uri(libraryAddress), "book"), bookJson);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
