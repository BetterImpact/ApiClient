using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("signed_document")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldSignedDocument : UserCustomFieldBase
    {
        public UserCustomFieldSignedDocument()
        {
        }

        public UserCustomFieldSignedDocument(string value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
