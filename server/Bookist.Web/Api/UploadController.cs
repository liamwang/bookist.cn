using Anet.Models;
using Bookist.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Api;

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
        var token = _qiniuService.GetToken();
        return new TypedValue<string>() { Value = token };
    }
}