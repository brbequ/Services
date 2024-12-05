namespace Library
{
    public class LibraryBook
    {
        public LibraryBook(string ISBN, string title, string author, string description, int quantity)
        {
            this.ISBN = ISBN;
            Author = author;
            Title = title;
            Description = description;
            Quantity = quantity;
        }

        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
