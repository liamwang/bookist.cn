namespace Bookist.Web.Models;

public class BookVM
{
    public string Title { get; set; }
    public string Cover { get; set; }
    public string Author { get; set; }
    public DateOnly PubDate { get; set; }
    public string Description { get; set; }
    public string Format { get; set; }
    public string FetchUrl { get; set; }
    public string FetchCode { get; set; }
}