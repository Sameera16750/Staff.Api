namespace Staff.Application.Helpers.SecurityHelper;

public interface ISecurityHelper
{
    string GenerateApiToken(string username);

    string EncryptValue(string value);

    string DecryptValue(string value);
}