using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NJsonSchema.Converters;

namespace FromSingapore.WebApi.Dtos;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(InheritedDto), nameof(InheritedDto))]
public class TestDto
{
    public string Nice { get; set; }
}

public class InheritedDto : TestDto
{
    public string Wtf { get; set; }
}