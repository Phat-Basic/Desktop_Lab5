using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int YearPublished { get; set; }
}
