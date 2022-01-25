using Anet;
using Bookist.Dtos;
using Bookist.Entities;
using Mapster;

namespace Bookist;

public class Mapping
{
    public static void Config()
    {
        TypeAdapterConfig<BookEditDto, Book>
            .ForType()
            .Ignore(x => x.Tags)
            .Map(des => des.Formats, src => string.Join('/', src.Formats))
            .AfterMapping(des =>
            {
                if (des.Id == 0) des.Id = IdGen.NewId();
                else des.UpdatedAt = DateTime.Now;
                des.Slug = UrlUtil.ParseSlug(des.Name, des.Id);
            });
    }
}
