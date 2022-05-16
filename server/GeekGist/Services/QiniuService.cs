using Microsoft.Extensions.Options;
using Qiniu.Storage;
using Qiniu.Util;

namespace GeekGist.Services;

public class QiniuService
{
    private readonly Mac _mac;
    private readonly Zone _zone;
    private readonly QiniuOptions _options;
    //private readonly HttpClient _httpClient;

    public QiniuService(IOptions<QiniuOptions> options) // , IHttpClientFactory httpClientFactory
    {

        _options = options.Value;
        _mac = new Mac(_options.AccessKey, _options.SecretKey);
        _zone = ZoneHelper.QueryZone(_options.AccessKey, _options.Bucket);
        //_httpClient = httpClientFactory.CreateClient();
    }

    public string GetUploadToken()
    {
        var policy = new PutPolicy { Scope = _options.Bucket };
        policy.SetExpires(60 * 3);
        policy.FsizeLimit = 3 * 1024 * 1024;
        policy.SaveKey = $"{DateTime.Now:yyyy/MM}/$(etag)$(ext)";
        policy.ReturnBody = $"{{\"key\":\"$(key)\",\"url\":\"{Config.ImageBaseUrl}$(key){ImageSize.SM.ToSuffix()}\"}}";
        policy.CallbackBody = "url=$(x:)";
        return Auth.CreateUploadToken(_mac, policy.ToJsonString());
    }

    public string UploadFile(Stream stream)
    {
        // todo:
        //var token = GetUploadToken();
        //var saveKey = $"{DateTime.Now:yyyy/MM}/$(etag)$(ext)";
        //var uploader = new FormUploader(new Qiniu.Storage.Config { Zone = _zone });
        //var result = uploader.UploadStream(stream, saveKey, token, null);
        return null;
    }

    public FetchInfo FetchUpload(string targetUrl, string key)
    {
        var config = new Qiniu.Storage.Config { Zone = _zone };
        var bm = new BucketManager(_mac, config);
        var result = bm.Fetch(targetUrl, _options.Bucket, key);
        if (result.Code != 200)
        {
            throw new HttpRequestException("自动存储图片失败！" + result.Text ?? result.RefText);
        }
        return result.Result;
    }
}
