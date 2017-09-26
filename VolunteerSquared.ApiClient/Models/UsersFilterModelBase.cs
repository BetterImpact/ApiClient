using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    public abstract class UsersFilterModelBase
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public bool IncludeCustomFields { get; set; } = true;
        public bool IncludeQualifications { get; set; } = true;
        public bool IncludeMemberships { get; set; } = true;
        public bool IncludeVerifiedVolunteersBackgroundCheckResults { get; set; } = true;

        public DateTime? UpdatedSince { get; set; }

        #region Modules

        internal string Modules
        {
            get
            {
                var modules = new List<string>();

                if (VolunteerModule) modules.Add("vol");
                if (ClientModule) modules.Add("cli");
                if (MemberModule) modules.Add("mem");
                if (DonorModule) modules.Add("don");
                if (AdminModule) modules.Add("admin");

                return string.Join(",", modules);
            }
        }

        internal bool HasModules
        {
            get
            {
                return !string.IsNullOrEmpty(Modules);
            }
        }

        public bool VolunteerModule { get; set; } = false;
        public bool ClientModule { get; set; } = false;
        public bool MemberModule { get; set; } = false;
        public bool DonorModule { get; set; } = false;
        public bool AdminModule { get; set; } = false;

        #endregion

        #region AdminStatus

        internal string AdminStatus
        {
            get
            {
                var statuses = new List<string>();

                if (AdminStatusActive) statuses.Add("active");
                if (AdminStatusArchived) statuses.Add("archived");

                return string.Join(",", statuses);
            }
        }

        internal bool HasAdminStatus
        {
            get
            {
                return !string.IsNullOrEmpty(AdminStatus);
            }
        }

        public bool AdminStatusActive { get; set; } = false;
        public bool AdminStatusArchived { get; set; } = false;

        #endregion

        #region ClientStatus

        internal string ClientStatus
        {
            get
            {
                var statuses = new List<string>();

                if (ClientStatusApplicant) statuses.Add("applicant");
                if (ClientStatusInProcess) statuses.Add("inprocess");
                if (ClientStatusAccepted) statuses.Add("accepted");
                if (ClientStatusInactive) statuses.Add("inactive");
                if (ClientStatusArchived) statuses.Add("archived");

                return string.Join(",", statuses);
            }
        }

        internal bool HasClientStatus
        {
            get
            {
                return !string.IsNullOrEmpty(ClientStatus);
            }
        }

        public bool ClientStatusApplicant { get; set; } = false;
        public bool ClientStatusInProcess { get; set; } = false;
        public bool ClientStatusAccepted { get; set; } = false;
        public bool ClientStatusInactive { get; set; } = false;
        public bool ClientStatusArchived { get; set; } = false;

        #endregion

        #region DonorStatus

        internal string DonorStatus
        {
            get
            {
                var statuses = new List<string>();

                if (DonorStatusProspect) statuses.Add("prospect");
                if (DonorStatusActive) statuses.Add("active");
                if (DonorStatusInactive) statuses.Add("inactive");
                if (DonorStatusArchived) statuses.Add("archived");

                return string.Join(",", statuses);
            }
        }

        internal bool HasDonorStatus
        {
            get
            {
                return !string.IsNullOrEmpty(DonorStatus);
            }
        }

        public bool DonorStatusProspect { get; set; } = false;
        public bool DonorStatusActive { get; set; } = false;
        public bool DonorStatusInactive { get; set; } = false;
        public bool DonorStatusArchived { get; set; } = false;

        #endregion

        #region MemberStatus

        internal string MemberStatus
        {
            get
            {
                var statuses = new List<string>();

                if (MemberStatusApplicant) statuses.Add("applicant");
                if (MemberStatusInProcess) statuses.Add("inprocess");
                if (MemberStatusAccepted) statuses.Add("accepted");
                if (MemberStatusInactive) statuses.Add("inactive");
                if (MemberStatusArchived) statuses.Add("archived");

                return string.Join(",", statuses);
            }
        }

        internal bool HasMemberStatus
        {
            get
            {
                return !string.IsNullOrEmpty(MemberStatus);
            }
        }

        public bool MemberStatusApplicant { get; set; } = false;
        public bool MemberStatusInProcess { get; set; } = false;
        public bool MemberStatusAccepted { get; set; } = false;
        public bool MemberStatusInactive { get; set; } = false;
        public bool MemberStatusArchived { get; set; } = false;

        #endregion

        #region VolunteerStatus

        internal string VolunteerStatus
        {
            get
            {
                var statuses = new List<string>();

                if (VolunteerStatusApplicant) statuses.Add("applicant");
                if (VolunteerStatusInProcess) statuses.Add("inprocess");
                if (VolunteerStatusAccepted) statuses.Add("accepted");
                if (VolunteerStatusInactiveShortTerm) statuses.Add("inactiveshortterm");
                if (VolunteerStatusInactiveLongTerm) statuses.Add("inactivelongterm");
                if (VolunteerStatusArchivedDidntStart) statuses.Add("archiveddidntstart");
                if (VolunteerStatusArchivedRejected) statuses.Add("archivedrejected");
                if (VolunteerStatusArchivedDismissed) statuses.Add("archiveddismissed");
                if (VolunteerStatusArchivedMoved) statuses.Add("archivedmoved");
                if (VolunteerStatusArchivedQuit) statuses.Add("archivedquit");
                if (VolunteerStatusArchivedDeceased) statuses.Add("archiveddeceased");
                if (VolunteerStatusArchivedOther) statuses.Add("archivedother");

                return string.Join(",", statuses);
            }
        }

        internal bool HasVolunteerStatus
        {
            get
            {
                return !string.IsNullOrEmpty(VolunteerStatus);
            }
        }

        public bool VolunteerStatusApplicant { get; set; } = false;
        public bool VolunteerStatusInProcess { get; set; } = false;
        public bool VolunteerStatusAccepted { get; set; } = false;
        public bool VolunteerStatusInactiveShortTerm { get; set; } = false;
        public bool VolunteerStatusInactiveLongTerm { get; set; } = false;
        public bool VolunteerStatusArchivedDidntStart { get; set; } = false;
        public bool VolunteerStatusArchivedRejected { get; set; } = false;
        public bool VolunteerStatusArchivedDismissed { get; set; } = false;
        public bool VolunteerStatusArchivedMoved { get; set; } = false;
        public bool VolunteerStatusArchivedQuit { get; set; } = false;
        public bool VolunteerStatusArchivedDeceased { get; set; } = false;
        public bool VolunteerStatusArchivedOther { get; set; } = false;

        #endregion
    }
}
