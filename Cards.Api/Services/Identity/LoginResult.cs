namespace Cards.Api.Services.Identity
{
    public class LoginResult
    {
        public LoginResult()
        {

        }

        public LoginResult(LoginResultCode loginResultCode)
        {
            this.LoginResultCode = loginResultCode;
        }

        public LoginResult(LoginResultCode loginResultCode, Data.Models.Dbo.UserProfile userProfile)
        {
            this.LoginResultCode = loginResultCode;
            this.UserProfile = userProfile;
        }

        public LoginResultCode LoginResultCode { get; set; }
        public Data.Models.Dbo.UserProfile UserProfile { get; set; }
    }

    public enum LoginResultCode
    {
        EmailNotFound = 1,
        InvalidPassword = 2,
        Success = 3
    }
}
