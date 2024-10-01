using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Tạo danh sách sách
        List<Book> books = new List<Book>
        {
            new Book
            {
                ISBN = "9831123212",
                Title = "A Programmer's Guide to ADO .Net using C#",
                Author = "Mahesh Chand",
                Price = 44.99m,
                YearPublished = 2002
            },
            new Book
            {
                ISBN = "9781484234",
                Title = "Pro Entity Framework Core 1",
                Author = "Adam Freeman",
                Price = 44.99m,
                YearPublished = 2018
            }
        };

        // Lưu vào file XML
        SaveToXmlFile(books);

        Console.WriteLine("Books have been saved to books.xml");
        Console.ReadKey();
    }

    private static void SaveToXmlFile(List<Book> books)
    {
        var serializer = new XmlSerializer(typeof(List<Book>));
        using (var writer = new StreamWriter("books.xml"))
        {
            serializer.Serialize(writer, books);
        }
    }
}
