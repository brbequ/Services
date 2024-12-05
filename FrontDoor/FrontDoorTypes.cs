namespace FrontDoor
{
    public class GetBooksResponse
    {
        public GetBooksResponse()
        {
            libraryBooks = string.Empty;
            bookStoreInventory = string.Empty;
        }

        public string libraryBooks { get; set; }

        public string bookStoreInventory { get; set; }
    }
}
