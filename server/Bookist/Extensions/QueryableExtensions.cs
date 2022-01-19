namespace System.Linq;

public static class QueryableExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> source, int page, int size)
    {
        return source.Skip((page - 1) * size).Take(size);
    }
}
