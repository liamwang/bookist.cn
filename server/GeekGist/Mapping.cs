using Anet;
using GeekGist.Dtos;
using GeekGist.Entities;
using Mapster;

namespace GeekGist;

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
            });
    }
}
