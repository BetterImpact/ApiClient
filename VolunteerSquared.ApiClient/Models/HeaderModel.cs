using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    public class HeaderModel
    {
        [JsonProperty("first_item_on_page")]
        public int FirstItemOnPage { get; set; }
        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }
        [JsonProperty("has_previous_page")]
        public bool HasPreviousPage { get; set; }
        [JsonProperty("is_first_page")]
        public bool IsFirstPage { get; set; }
        [JsonProperty("is_last_page")]
        public bool IsLastPage { get; set; }
        [JsonProperty("last_item_on_page")]
        public int LastItemOnPage { get; set; }
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        [JsonProperty("total_item_count")]
        public int TotalItemCount { get; set; }
    }
}
