using System.Reflection;
using System.Text.Json.Serialization;
using AuthorizationLibrary.Configuration;
using CEC.DL.Evaluation.ManagementService.SwaggerExtensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMamAuthorization(builder.Configuration);

const string customPolicyName = "Custom";
builder.Services.AddCors(options => options.AddPolicy(name: customPolicyName,
    corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyHeader()
                         .AllowAnyMethod()
                         .SetIsOriginAllowed(_ => true)
                         .AllowCredentials();
    }));

builder.Services.AddSingleton<IJsonHierarchyRootsProvider, JsonHierarchyRootsProvider>();
builder.Services.AddSingleton<IJsonHierarchiesProvider, JsonHierarchiesProvider>();
builder.Services.AddSingleton<IJsonHierarchiesRepository, JsonHierarchiesRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AuthorizationService",
        Description = "Authorization Service for MAM"
    });
    
    c.UseOneOfForPolymorphism();
    c.SchemaFilter<TypeDiscriminatorSchemaFilter>();
    c.SelectDiscriminatorNameUsing(_ => "$type");
    c.SelectDiscriminatorValueUsing(subType => subType.BaseType!
                                                      .GetCustomAttributes<JsonDerivedTypeAttribute>()
                                                      .FirstOrDefault(x => x.DerivedType == subType)?
                                                      .TypeDiscriminator!.ToString());
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors(customPolicyName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
