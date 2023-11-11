using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Scraping
{
    class Book
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
    }
    class ScrapingClass
    {
        public static List<Book> Call()
        {
            //List<string>? booklinks = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("BooksDataBase.json"));
            List<string> booklinks = GetBoooLinks();
            File.WriteAllText("BooksDataBase.json", JsonSerializer.Serialize(booklinks, new JsonSerializerOptions() { WriteIndented = true }));
            return GetBookDetails(booklinks);

        }
        static List<Book> GetBookDetails(List<string> urls)
        {

            List<Book> books = new List<Book>();
            foreach (string url in urls)
            {
                HtmlDocument document = default;
                try
                {
                     document = GetDocument(url);

                }
                catch (Exception)
                {
                }                string titleXPath = "//h1";
              if(document!=null)
                {
                    string imageXPath = "//img";

                    Book book = new Book();
                    book.Title = document.DocumentNode.SelectSingleNode(titleXPath)?.InnerText;

                    string imageUrl = document.DocumentNode.SelectSingleNode(imageXPath)?.Attributes["src"]?.Value;
                    book.ImageUrl = new Uri(new Uri(url), imageUrl)?.AbsoluteUri;

                    books.Add(book);
                }
            }
            File.WriteAllText("BooksDataBase.json", JsonSerializer.Serialize(books, new JsonSerializerOptions() { WriteIndented = true }));

            return books;
        }
        static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        static List<string> GetBoooLinks()
        {
            List<string> bookLinks = new();
            string? url = default;
            //for (int i = 1; i < 51; i++)
            //{
            url = $"https://turbo.az/autos?q%5Bmake%5D%5B%5D=10";

            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection linknodes = doc.DocumentNode.SelectNodes(xpath: "//div/a");
            Uri baseUri = new Uri(uriString: url);
            foreach (HtmlNode link in linknodes)
            {
                string href = link.Attributes["href"].Value;
                bookLinks.Add(item: new Uri(baseUri, relativeUri: href).AbsoluteUri);
            }
            //}
            return bookLinks;
        }
    }
}
