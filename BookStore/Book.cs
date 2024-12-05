namespace BookStore
{
    public class Book
    {
        public Book(
            string ISBN, DateOnly publishDate, string title, string author, string description,
            string bookFormat, int pages, float price, int quantityInStock)
        {
            this.ISBN = ISBN;
            PublishDate = publishDate;
            Title = title;
            Author = author;
            Description = description;
            BookFormat = bookFormat;
            Pages = pages;
            USDPrice = price;
            QuantityInStock = quantityInStock;
        }

        public string ISBN {  get; set; }
        public DateOnly PublishDate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string BookFormat { get; set; }
        public int Pages { get; set; }
        public float USDPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
