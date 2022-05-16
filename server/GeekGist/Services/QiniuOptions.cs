namespace GeekGist.Services;

public class QiniuOptions
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Bucket { get; set; }
    public string Zone { get; set; }

    /// <summary>
    /// Token过期时长（单位：秒，默认：10秒）
    /// </summary>
    public int TokenExpireSeconds { get; set; } = 10;
}

