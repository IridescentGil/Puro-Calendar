using System;
using HtmlAgilityPack;

namespace Puro_Calendar;

public class Stardorm_Calendar : IScraper
{   
    public event EventHandler<ScraperEventArgs>? IsCurrentlyScraping;
    public bool isScraping = false;
    public bool IsScraping { get => isScraping; 
    set
        {
            isScraping = value;
            OnIsCurrentlyScraping(new ScraperEventArgs(IsScraping));
        } }

    public Stardorm_Calendar()
    {
    }
    public void Scrape()
    {
        IsScraping = true;
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        var web = new HtmlWeb();

        var doc = web.Load($"https://wwr-stardom.com/schedule/{currentDate.Year}{currentDate.Month:00}/");
        var events = doc.DocumentNode.SelectNodes("//li[.//div[@class='box_game']]");
        foreach (var ev in events)
        {
            var date = ev.SelectSingleNode(".//span[contains(@class, 'date')]").InnerText.Trim();
            var title = ev.SelectSingleNode(".//div[contains(@class, 'box_game')]").InnerText.Trim();
            string link = ev.SelectSingleNode(".//a").GetAttributeValue("href", string.Empty);
            if (!string.IsNullOrEmpty(link))
            {
                var eventDoc = web.Load(link);
                var location = eventDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'schedule__place')]").InnerText.Trim();
                var description = eventDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'schedule__detail')]").InnerText.Trim();
                Console.WriteLine($"{date} - {title} - {location} - {description}");
            }
            //var location = ev.SelectSingleNode(".//div[contains(@class, 'schedule__place')]").InnerText.Trim();
            //var description = ev.SelectSingleNode(".//div[contains(@class, 'schedule__detail')]").InnerText.Trim();
            //Console.WriteLine($"{date} - {title} - {location} - {description}");
        }
        IsScraping = false;
    }
    protected virtual void OnIsCurrentlyScraping(ScraperEventArgs e)
    {
        IsCurrentlyScraping?.Invoke(this, e);
    }
}
