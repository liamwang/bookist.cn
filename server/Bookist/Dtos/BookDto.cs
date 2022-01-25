using Bookist.Entities;

namespace Bookist.Dtos;

public class BookDto : Book
{
    public string PubDateStr => PubDate.ToString("yyyy-MM");
    public string CoverUrl => Cover == null ? null : UrlUtil.Image(Cover, ImageSize.MD);
    public string CoverUrlSm => Cover == null ? null : UrlUtil.Image(Cover, ImageSize.SM);
}