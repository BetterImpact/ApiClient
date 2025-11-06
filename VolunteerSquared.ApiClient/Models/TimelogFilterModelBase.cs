using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    public class TimelogFilterModelBase
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool IncludeRecordedFeedbackFields { get; set; }

        public ApprovedStatus FilterApprovedStatus { get; set; } = ApprovedStatus.DontFilter;

        internal string FilterApprovedStatusString 
        {
            get
            {
                switch (FilterApprovedStatus)
                {
                    case ApprovedStatus.DontFilter:
                        return "dontfilter";
                    case ApprovedStatus.ApprovedOnly:
                        return "approved";
                    case ApprovedStatus.UnapprovedOnly:
                        return "unapproved";
                    default:
                        throw new ArgumentException(nameof(FilterApprovedStatus));
                }
            }
        }

        public List<int> FilterUserIds { get; set; } = new List<int>();

        internal string FilterUserIdsString 
        {
            get
            {
                return string.Join(",", FilterUserIds);
            }
        }

        internal bool HasFilterUserIds
        {
            get
            {
                return FilterUserIds != null && FilterUserIds.Any();
            }
        }

        public List<int> FilterActivityIds { get; set; } = new List<int>();

        internal string FilterActivityIdsString
        {
            get
            {
                return string.Join(",", FilterActivityIds);
            }
        }

        internal bool HasFilterActivityIds
        {
            get
            {
                return FilterActivityIds != null && FilterActivityIds.Any();
            }
        }

        public List<int> FilterActivityCategoryIds { get; set; } = new List<int>();

        internal string FilterActivityCategoryIdsString
        {
            get
            {
                return string.Join(",", FilterActivityCategoryIds);
            }
        }

        internal bool HasFilterActivityCategoryIds
        {
            get
            {
                return FilterActivityCategoryIds != null && FilterActivityCategoryIds.Any();
            }
        }

        public List<int> FilterActivityReportGroupIds { get; set; } = new List<int>();

        internal string FilterActivityReportGroupIdsString
        {
            get
            {
                return string.Join(",", FilterActivityReportGroupIds);
            }
        }

        internal bool HasFilterActivityReportGroupIds
        {
            get
            {
                return FilterActivityReportGroupIds != null && FilterActivityReportGroupIds.Any();
            }
        }

        public DateTime? UpdatedSince { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

        public DateTime? WorkedFrom { get; set; }

        public DateTime? WorkedTo { get; set; }
    }
}
