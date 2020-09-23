using System.ComponentModel.DataAnnotations;

namespace PCLoan.Presentation.Web.Models
{
    /// <summary>
    /// A model to hold login data
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The username used to login
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string UserName { get; set; }

        /// <summary>
        /// The Password used to log in
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Password { get; set; }
    }
}
