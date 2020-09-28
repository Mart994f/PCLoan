using System.DirectoryServices.AccountManagement;

namespace PCLoan.Logic.Library.Models
{
    /// <summary>
    /// A model representing a user.
    /// </summary>
    public class UserModelDTO
    {
        /// <summary>
        /// The users ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The users username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The users password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// If the user is authenticated.
        /// </summary>
        public bool Authenticated { get; set; } = false;

        /// <summary>
        /// The role of the user.
        /// </summary>
        public string Role { get; set; } = null;

        /// <summary>
        /// The JWS for the user once authenticated & authorized
        /// </summary>
        public string Token { get; set; } = null;

        /// <summary>
        /// The <see cref="UserPrincipal"/> of the user.
        /// </summary>
        public UserPrincipal UserPrincipal { get; set; } = null;
    }
}
