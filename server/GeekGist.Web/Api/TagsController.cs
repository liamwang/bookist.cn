using GeekGist.Dtos;
using GeekGist.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekGist.Web.Api;

[ApiAuth]
[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly TagService _tagService;

    public TagsController(TagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IEnumerable<TagDto>> Get(string keyword = null)
    {
        return await _tagService.GetAsync(keyword);
    }

    [HttpPost]
    public Task Save(TagEditDto dto)
    {
        return _tagService.SaveAsync(dto);
    }

    [HttpDelete("{id}")]
    public Task Delete(long id)
    {
        return _tagService.DeleteAsync(id);
    }
}
