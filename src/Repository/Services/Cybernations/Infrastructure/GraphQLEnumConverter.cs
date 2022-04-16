using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Repository.Services.Cybernations.Infrastructure;

public sealed class GraphQLEnumConverter : StringEnumConverter
{
    public GraphQLEnumConverter() : base(new ConstantCaseNamingStrategy())
    {
    }
}

internal sealed class ConstantCaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name) => StringUtils.ToConstantCase(name);
}