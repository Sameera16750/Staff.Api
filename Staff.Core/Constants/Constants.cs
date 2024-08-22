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
        }

        #endregion
    }
    

    #endregion

    #region Status

    public static class Status
    {
        public static readonly int Active = 1;
        public static readonly int Deleted = 0;
    }

    #endregion

    #region Process Status

    public static class ProcessStatus
    {
        public static readonly int Success = 1;
        public static readonly int Failed= 0;
        public static readonly int NotFound = -1;
    }

    #endregion
    #region Files

    public static class Files
    {
        #region Resource Files

        public static readonly string Messages="Staff.Application.Resources.MessageResources";

        #endregion
    }

    #endregion
}