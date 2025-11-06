using Newtonsoft.Json;
using System;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    //this has to be here because otherwise the deserialization process tries to instantiate this class, which doesnt work.
    public abstract class CustomFieldBase
    {
        [JsonProperty("custom_field_id")]
        public int CustomFieldId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty("belongs_to_enterprise")]
        public bool BelongsToEnterprise { get; set; }

        [JsonProperty("custom_field_category_id")]
        public int? CustomFieldCategoryId { get; set; }
        [JsonProperty("custom_field_category_name")]
        public string CustomFieldCategoryName { get; set; }
        [JsonProperty("custom_field_category_display_order")]
        public int? CustomFieldCategoryDisplayOrder { get; set; }
    }
}
