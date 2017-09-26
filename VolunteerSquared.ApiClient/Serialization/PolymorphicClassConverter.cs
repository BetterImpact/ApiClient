using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Serialization
{
    public class PolymorphicClassConverter : JsonConverter
    {
        private Dictionary<string, Type> nameToType { get; set; }
        private Dictionary<Type, string> typeToName { get; set; }

        public PolymorphicClassConverter()
        {
            var customDisplayNameTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetCustomAttributes(false).Any(y => y is DisplayNameAttribute));

            nameToType = customDisplayNameTypes.ToDictionary(t => t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().First().DisplayName, t => t);
            typeToName = nameToType.ToDictionary(t => t.Value, t => t.Key);
        }

        public override bool CanRead { get; } = true;
        public override bool CanWrite { get; } = false;

        public override bool CanConvert(Type objectType)
        {
            return typeToName.ContainsKey(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            dynamic jsonObject = JObject.Load(reader);
            dynamic returnObject = Activator.CreateInstance(nameToType[jsonObject["type"].Value as string]);

            serializer.Populate(jsonObject.CreateReader(), returnObject);

            return returnObject;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
