using PCLoan.Presentation.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCLoan.Presentation.Web.Models
{
    /// <summary>
    /// A model representing a user.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The users ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The users username.
        /// </summary>
        [Required] 
        public string UserName { get; set; }

        /// <summary>
        /// The users password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// If the user is authenticated.
        /// </summary>
        public bool Authenticated { get; set; } = false;

        /// <summary>
        /// The role of the user.
        /// </summary>
        public Role? Role { get; set; } = null;
    }
}
