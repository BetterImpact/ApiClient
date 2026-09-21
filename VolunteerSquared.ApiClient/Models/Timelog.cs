using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class Timelog
    {
        [JsonProperty("timelog_entry_id")]
        public int TimelogEntryId { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }

        [JsonProperty("timelog_entry_type")]
        public string TimelogEntryType { get; set; }

        [JsonProperty("date_worked")]
        public DateTime DateWorked { get; set; }

        [JsonProperty("hours_worked")]
        public double HoursWorked { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("clock_start_time")]
        public DateTime? ClockStartTime { get; set; }

        [JsonProperty("time_clock_auto_stopped")]
        public bool TimeClockAutoStopped { get; set; }

        [JsonProperty("activity_id")]
        public int ActivityId { get; set; }

        [JsonProperty("activity_name")]
        public string ActivityName { get; set; }

        [JsonProperty("activity_category_id")]
        public int? ActivityCategoryId { get; set; }

        [JsonProperty("activity_category_name")]
        public string ActivityCategoryName { get; set; }

        [JsonProperty("activity_report_group_id")]
        public int? ActivityReportGroupId { get; set; }

        [JsonProperty("activity_report_group_name")]
        public string ActivityReportGroupName { get; set; }

        [JsonProperty("organization_id")]
        public int OrganizationId { get; set; }

        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("created_by_user_id")]
        public int? CreatedByUserId { get; set; }

        [JsonProperty("created_by_first_name")]
        public string CreatedByFirstName { get; set; }

        [JsonProperty("created_by_last_name")]
        public string CreatedByLastName { get; set; }

        [JsonProperty("recorded_feedback_fields")]
        public List<RecordedFeedbackFieldBase> RecordedFeedbackFields { get; set; }
    }
}
