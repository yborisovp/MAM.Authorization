using CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models.Discriminators;

namespace CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models;

public record DerivedTypeInfo(
    Type Type,
    IDiscriminator Discriminator
);

public record JsonHierarchy(
    Type BaseType,
    Dictionary<Type, DerivedTypeInfo> DerivedTypes
);