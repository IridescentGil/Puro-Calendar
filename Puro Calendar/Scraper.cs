using System;

namespace Puro_Calendar;

public interface IScraper
{
    public event EventHandler<ScraperEventArgs> IsCurrentlyScraping;

    public void Scrape();
}

public class ScraperEventArgs(bool isScraping) : EventArgs
{
    public bool IsScraping { get; set; } = isScraping;
}

