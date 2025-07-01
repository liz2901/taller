using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Encuba.Product.Infrastructure.Middlewares;

public class PrivateSetterContractResolver : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(
        MemberInfo member,
        MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);
        if (!property.Writable)
        {
            PropertyInfo propertyInfo = member as PropertyInfo;
            if ((object) propertyInfo != null)
                property.Writable = propertyInfo.GetSetMethod(true) != (MethodInfo) null;
        }
        return property;
    }
}