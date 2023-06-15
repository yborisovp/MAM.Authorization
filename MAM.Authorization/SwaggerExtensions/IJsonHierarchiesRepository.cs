using CEC.DL.Evaluation.ManagementService.SwaggerExtensions;
using JsonHierarchy = CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models.JsonHierarchy;

namespace MAM.Authorization.SwaggerExtensions;

public interface IJsonHierarchiesRepository
{
    bool TryGetHierarchy(Type baseType, out JsonHierarchy jsonHierarchy);
}

internal class JsonHierarchiesRepository : IJsonHierarchiesRepository
{
    private readonly Dictionary<Type, JsonHierarchy> _hierarchies;

    public JsonHierarchiesRepository(IJsonHierarchiesProvider provider) =>
        _hierarchies = provider.GetAllHierarchies()
            .ToDictionary(h => h.BaseType, h => h);

    public bool TryGetHierarchy(Type baseType, out JsonHierarchy jsonHierarchy)
    {
        jsonHierarchy = null;
        return baseType is null ? false : _hierarchies.TryGetValue(baseType, out jsonHierarchy!);
    }
}