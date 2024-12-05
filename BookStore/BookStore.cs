namespace BookStore
{
    public class BookStore
    {
        private List<Book> books;

        public BookStore()
        {
            books = new List<Book>();
            StockShelf();
        }

        public IEnumerable<Book> GetInventory()
        {
            return books.AsReadOnly();
        }

        private void StockShelf()
        {
            books.Add(new Book(
                "ISBN 978-1-6680-5227-3",
                DateOnly.Parse("09/15/2024"),
                "War",
                "Bob Woodward",
                "War is an intimate and sweeping account of one of the most tumultuous periods in presidential politics and American history.",
                "Hardcover",
                435,
                15.99F,
                21));
            books.Add(new Book(
                String.Empty,
                DateOnly.Parse("09/15/1977"),
                "The Sword of Shanarra",
                "Terry Brooks",
                "The tale of the Warlock Lord and the last son of the house of Shanarra",
                "Hardcover",
                718,
                12.95F,
                7));
            books.Add(new Book(
                String.Empty,
                DateOnly.Parse("01/01/1958"),
                "The Once and Future King",
                "T. H. White",
                "The tale of King Arthur and Merlin",
                "Hardcover",
                631,
                21.00F,
                0));
            books.Add(new Book(
                "ISBN 978-0-385-33597-3",
                DateOnly.Parse("01/01/1992"),
                "Dragonfly In Amber",
                "Diana Gabaldon",
                "Twenty years later, Claire Randall returns to the Scottish Highlands.",
                "Paperback",
                743,
                18.00F,
                12));
        }
    }
}
