using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.Connections;

namespace Library
{
    public static class Library
    {
        private static List<LibraryBook> books = new();

        public static LibraryBook AddBook(LibraryBook newBook)
        {
            if (books.Count == 0)
                StockShelf();

            var book = GetBook(newBook.ISBN);
            if (book != null)
            {
                book.Quantity += newBook.Quantity;
            }
            else
            {
                books.Add(newBook);
                book = newBook;
            }
            return book;
        }

        public static LibraryBook? GetBook(string isbn)
        {
            if (books.Count == 0)
                StockShelf();

            return books.Find(x => x.ISBN == isbn);
        }

        public static IEnumerable<LibraryBook> GetBooks()
        {
            if (books.Count == 0)
                StockShelf();

            return books.AsReadOnly();
        }

        private static void StockShelf()
        {
            books.Add(new LibraryBook(
                "978-1-6680-5227-3",
                "War",
                "Bob Woodward",
                "War is an intimate and sweeping account of one of the most tumultuous periods in presidential politics and American history.",
                1));
            books.Add(new LibraryBook(
                "978-0-5937-2543-6",
                "The Sword of Shanarra",
                "Terry Brooks",
                "The tale of the Warlock Lord and the last son of the house of Shanarra",
                2));
            books.Add(new LibraryBook(
                "978-0-1431-1161-0",
                "The Once and Future King",
                "T. H. White",
                "The tale of King Arthur and Merlin",
                0));
            books.Add(new LibraryBook(
                "978-0-385-33597-3",
                "Dragonfly In Amber",
                "Diana Gabaldon",
                "Twenty years later, Claire Randall returns to the Scottish Highlands.",
                4));
        }
    }
}
