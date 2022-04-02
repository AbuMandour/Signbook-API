using System;
namespace SignBookProject.Constants
{
    public static class Uris
    {
        public static string SendbirdUri(string applicationId) => $"https://api-{applicationId}.sendbird.com/v3/users";
    }
}
