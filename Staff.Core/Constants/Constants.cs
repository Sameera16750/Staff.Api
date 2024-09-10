namespace Staff.Core.Constants;

public static class Constants
{
    #region Messages

    public static class Messages
    {
        #region Success Messages

        public static class Success
        {
            public const string SaveSuccess = "SAVE_SUCCESS";
            public const string UpdateSuccess = "UPDATE_SUCCESS";
            public const string DeleteSuccess = "DELETE_SUCCESS";
            public const string CheckInSuccess = "CHECK_IN_SUCCESS";
            public const string CheckOtSuccess = "CHECK_OUT_SUCCESS";
        }

        #endregion

        #region Error Messages

        public static class Error
        {
            public const string SaveFailed = "SAVE_FAILD";
            public const string UpdateFailed = "UPDATE_FAILED";
            public const string DeleteFailed = "DELETE_FAILED";
            public const string DataNotFound = "DATA_NOT_FOUND";
            public const string InternalServerError = "INTERNAL_ERROR";
            public const string InvalidOrganization = "INVALID_ORGANIZATION";
            public const string InvalidDepartment = "INVALID_DEPARTMENT";
            public const string DepartmentExist = "DEPARTMENT_EXISTS";
            public const string InvalidDesignation = "INVALID_DESIGNATION";
            public const string DesignationExists = "DESIGNATION_EXISTS";
            public const string AgeMinimum = "AGE_MINIMUM";
            public const string AgeMaximum = "AGE_MAXIMUM";
            public const string InvalidStaff = "INVALID_STAFF";
            public const string SameStaffUsageForReviewer = "SAME_STAFF_USAGE_FOR_REVIEWER";
            public const string InvalidReview = "INVALID_REVIEW";
            public const string CheckInFailed = "CHECK_IN_FAILED";
            public const string CheckOtFailed = "CHECK_OUT_FAILED";
        }

        #endregion
    }

    #endregion

    #region Status

    public static class Status
    {
        public static readonly int Active = 1;
        public static readonly int Deleted = 0;
        public static readonly int All = -10;
    }

    #endregion

    #region Process Status

    public static class ProcessStatus
    {
        public static readonly int Success = 1;
        public static readonly int Failed = 0;
        public static readonly int NotFound = -1;
        public static readonly int AlreadyExists = -2;
    }

    #endregion

    #region Files

    public static class Files
    {
        #region Resource Files

        public static readonly string Messages = "Staff.Application.Resources.MessageResources";

        #endregion
    }

    #endregion

    #region Headers

    public static class Headers
    {
        public static readonly string ApiKeyHeader = "X-Api-Key";
        public static readonly string OrganizationId = "OrganizationId";
    }

    #endregion
}