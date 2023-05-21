using System.Reflection;
using System.Text.Json.Serialization;
using JsonHierarchyRoot = CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models.JsonHierarchyRoot;

namespace CEC.DL.Evaluation.ManagementService.SwaggerExtensions;

public interface IJsonHierarchyRootsProvider
{
    IEnumerable<JsonHierarchyRoot> GetAllRoots();
}

internal class JsonHierarchyRootsProvider : IJsonHierarchyRootsProvider
{

    public JsonHierarchyRootsProvider()
    {
    }

    public IEnumerable<JsonHierarchyRoot> GetAllRoots() =>
        Assembly.GetExecutingAssembly().GetReferencedAssemblies().SelectMany(assembly => Assembly.Load(assembly)
            .GetTypes().Select(type => new JsonHierarchyRoot(
                type, type.GetCustomAttributes<JsonDerivedTypeAttribute>()
            )).Where(x => x.Attributes.Any()));
}