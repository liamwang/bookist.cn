namespace Bookist.Dtos;

public class TagDto
{
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 处理后标题（用于构建URL）
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// 书籍数量
    /// </summary>
    public int BookCount { get; set; }
}