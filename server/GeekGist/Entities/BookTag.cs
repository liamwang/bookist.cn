namespace GeekGist.Entities;

public class BookTag
{
    public long BookId { get; set; }
    public long TagId { get; set; }

    public Book Book { get; set; }
    public Tag Tag { get; set; }
}
