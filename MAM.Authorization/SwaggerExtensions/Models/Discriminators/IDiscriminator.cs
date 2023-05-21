using Microsoft.OpenApi.Any;

namespace CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models.Discriminators;

public interface IDiscriminator
{
    string Type { get; }
    
    IOpenApiAny Value { get; }
}