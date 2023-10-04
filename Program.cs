using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

public class Event
{
    public string Date { get; set; }
    public string Result { get; set; }
    public string IpFrom { get; set; }
    public string Method { get; set; }
    public string UrlTo { get; set; }
    public string Response { get; set; }
}

public class Log
{
    public List<Event> Events { get; set; } = new List<Event>();
}

public static class XmlParser
{
    public static Log ParseXmlToLog(string xmlString)
    {
        XDocument doc = XDocument.Parse(xmlString);
        Log log = new Log();

        foreach (XElement eventElement in doc.Descendants("event"))
        {
            Event e = new Event
            {
                Date = eventElement.Attribute("date")?.Value.Trim(),
                Result = eventElement.Attribute("result")?.Value.Trim(),
                IpFrom = eventElement.Element("ip-from")?.Value.Trim(),
                Method = eventElement.Element("method")?.Value.Trim(),
                UrlTo = eventElement.Element("url-to")?.Value.Trim(),
                Response = eventElement.Element("response")?.Value.Trim()
            };

            log.Events.Add(e);
        }

        return log;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string xmlFilePath = @"C:\Users\plesk\source\repos\ConsoleApp5\XMLFile1.xml";
        string xmlString = System.IO.File.ReadAllText(xmlFilePath);

        Log log = XmlParser.ParseXmlToLog(xmlString);

        foreach (var e in log.Events)
        {
            Console.WriteLine($"Date: {e.Date}");
            Console.WriteLine($"Result: {e.Result}");
            Console.WriteLine($"IP From: {e.IpFrom}");
            Console.WriteLine($"Method: {e.Method}");
            Console.WriteLine($"URL To: {e.UrlTo}");
            Console.WriteLine($"Response: {e.Response}");
            Console.WriteLine();
        }
    }
}
