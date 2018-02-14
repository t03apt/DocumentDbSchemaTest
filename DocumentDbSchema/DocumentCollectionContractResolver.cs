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
        private IList<string> _propertiesToIgnore = new[] {
            nameof(Resource.ResourceId),
            nameof(Resource.ETag),
            nameof(Resource.AltLink),
            nameof(Resource.Timestamp),
            nameof(Resource.SelfLink),
            nameof(DocumentCollection.UniqueKeyPolicy),
            nameof(DocumentCollection.DefaultTimeToLive)
        };

        private IList<string> _requiredProperties = new[] {
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
                !HasPublicGetter(property) ||
                _propertiesToIgnore.Contains(property.Name)
                )
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

        private static bool HasPublicGetter(PropertyInfo property)
        {
            return property.GetGetMethod() != null;
        }

        private static bool HasPublicSetter(PropertyInfo property)
        {
            return property.GetSetMethod() != null;
        }

        private bool IsRequiredProperty(PropertyInfo property)
        {
            return _requiredProperties.Contains(property.Name);
        }
    }
}
