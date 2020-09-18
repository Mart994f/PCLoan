using PCLoan.Logic.Library.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.AccountManagement;
using System.Security;

namespace PCLoan.Logic.Library.Models
{
    /// <summary>
    /// A model representing a user.
    /// </summary>
    [Table("User")]
    public class UserModel
    {
        /// <summary>
        /// The users ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The users username.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        /// <summary>
        /// The users password.
        /// </summary>
        [NotMapped]
        public string Password { get; set; }

        /// <summary>
        /// If the user is authenticated.
        /// </summary>
        [NotMapped]
        public bool Authenticated { get; set; } = false;

        /// <summary>
        /// The role of the user.
        /// </summary>
        [NotMapped]
        public Role? Role { get; set; } = null;

        /// <summary>
        /// The <see cref="UserPrincipal"/> of the user.
        /// </summary>
        [NotMapped]
        public UserPrincipal UserPrincipal { get; set; } = null;
    }
}
