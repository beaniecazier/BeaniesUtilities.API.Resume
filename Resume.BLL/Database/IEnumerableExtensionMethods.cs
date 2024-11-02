using BeaniesUtilities.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.Resume.BLL.Database;

public static class IEnumerableExtensionMethods
{
    public static IEnumerable<T> GroupByAndFindLatest<T>(this IEnumerable<T> entries) where T : BaseModel
    {
        var groups = entries.GroupBy(x => x.CommonIdentity);

        List<T> models = new List<T>();
        foreach (var group in groups)
        {
            var lastModified = group.Max(x => x.ModifiedOn);
            var model = group.Where(x => x.ModifiedOn == lastModified).SingleOrDefault();
            models.Add(model);
        }
        return models;
    }

    public static IEnumerable<T> Replace<T>(this IEnumerable<T> entries, int id, T value) where T : BaseModel
    {
        int current = 0;
        foreach (var entry in entries)
        {
            yield return current == id ? value : entry;
            current++;
        }
    }

    public static IEnumerable<T> Replace<T>(this IEnumerable<T> entries, IEnumerable<T> values) where T : BaseModel
    {
        int current = 0;
        var ids = entries.Select(x=>x.EntryIdentity);
        foreach (var entry in entries)
        {
            yield return ids.Contains(current) ? values.Where(x=>x.EntryIdentity == current).First() : entry;
        }
    }
}
