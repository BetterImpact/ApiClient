using Newtonsoft.Json;
using System;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    //this has to be here because otherwise the deserialization process tries to instantiate this class, which doesnt work.
    public abstract class UserCustomFieldBase
    {
        [JsonProperty("custom_field_id")]
        public int CustomFieldId { get; set; }
        [JsonProperty("custom_field_name")]
        public string CustomFieldName { get; set; }

        [JsonProperty("custom_field_category_id")]
        public int? CustomFieldCategoryId { get; set; }
        [JsonProperty("custom_field_category_name")]
        public string CustomFieldCategoryName { get; set; }

        //TODO: more fields (permissions, displays on, modules, etc.)
    }

}
