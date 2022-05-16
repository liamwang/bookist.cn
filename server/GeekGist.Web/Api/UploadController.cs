using Anet.Models;
using GeekGist.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekGist.Web.Api;

[ApiAuth]
[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly QiniuService _qiniuService;

    public UploadController(QiniuService qiniuService)
    {
        _qiniuService = qiniuService;
    }

    [HttpGet("[action]")]
    public TypedValue<string> QiniuToken()
    {
        var token = _qiniuService.GetUploadToken();
        return new TypedValue<string>() { Value = token };
    }
}