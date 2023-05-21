using Microsoft.OpenApi.Any;

namespace CEC.DL.Evaluation.ManagementService.SwaggerExtensions.Models.Discriminators;

internal record StringDiscriminator(string StrValue) : IDiscriminator
{
    public string Type => "string";

    public IOpenApiAny Value =>
        new OpenApiString(StrValue);
    
    public override string ToString() =>
        StrValue;
}