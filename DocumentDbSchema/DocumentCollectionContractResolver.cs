using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocumentDbSchema
{
    public class DocumentCollectionContractResolver : CamelCasePropertyNamesContractResolver
    {
        private static IList<string> PropertiesToIgnore = new[] {
            nameof(Resource.ResourceId),
            nameof(Resource.ETag),
            nameof(Resource.AltLink),
            nameof(Resource.Timestamp),
            nameof(Resource.SelfLink),
            nameof(DocumentCollection.UniqueKeyPolicy),
            nameof(DocumentCollection.DefaultTimeToLive)
        };

        private static IList<string> RequiredProperties = new[] {
            nameof(Resource.Id)
        };

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization)
                .Where(o => o.Writable)
                .OrderBy(o => o.PropertyName);

            return properties.ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            if (!(member is PropertyInfo property) ||
                ShouldIgnoreProperty(property) ||
                !HasPublicGetter(property))
            {
                return null;
            }

            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (IsRequiredProperty(property))
            {
                jsonProperty.Required = Required.Always;
            }
            else if (jsonProperty.Required == Required.Default)
            {
                jsonProperty.Required = Required.DisallowNull;
            }

            return jsonProperty;
        }

        private static bool HasPublicGetter(PropertyInfo property) => property.GetGetMethod() != null;

        private static bool IsRequiredProperty(PropertyInfo property) => RequiredProperties.Contains(property.Name);

        private static bool ShouldIgnoreProperty(PropertyInfo property) => PropertiesToIgnore.Contains(property.Name);
    }
}
