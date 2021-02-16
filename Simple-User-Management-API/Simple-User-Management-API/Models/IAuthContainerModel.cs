using System.Security.Claims;

namespace Simple_User_Management_API.Models
{
    public interface IAuthContainerModel
    {
        #region Members

        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }

        Claim[] Claims { get; set; }

        #endregion Members
    }
}