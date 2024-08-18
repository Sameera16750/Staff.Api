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
        }

        #endregion
        
        #region Error Messages

        public static class Error
        {
            public const string SaveFailed = "SAVE_FAILD";
            public const string DataNotFound = "DATA_NOT_FOUND";
            public const string InternalServerError = "INTERNAL_ERROR";
        }

        #endregion
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