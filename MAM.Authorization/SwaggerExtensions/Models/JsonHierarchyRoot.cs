using System.Text.Json.Serialization;

namespace CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models;

public record JsonHierarchyRoot(
    Type Type,
    IEnumerable<JsonDerivedTypeAttribute> Attributes
);